using UnityEngine;

public class EnemyAIController : StateController 
{
    private FieldOfView view;

    protected override void Awake()
    {
        base.Awake();
        view = GetComponentInChildren<FieldOfView>();
    }

    protected override void Start()
    {
        actor.isControlledByAI = true;
        actor.patrolViewRangeX = view.sizeX;
        actor.chaseViewRangeX = view.sizeX * 2;
        currentState = new PatrolState(this);
    }

    public bool IsTargetInSight()
    {
        return view.isTargetInSight;
	}

    public Transform GetTargetTransform()
    {
        return view.targetTransform;
	}

    public void ChangeToPatrolView()
    {
        view.sizeX = actor.patrolViewRangeX;
	}

    public void ChangeToChaseView()
    {
        view.sizeX = actor.chaseViewRangeX;
	}

    public void SetViewGizmoColor(Color aColor)
    {
        view.gizmoColor = aColor;
	}

    public bool IsTargetInAttackRange()
    { 
        if (GetTargetTransform() != null && (transform.position - GetTargetTransform().position).magnitude <= actor.attackRange)
        {
            return true;
		}
        return false;
	}

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        if (currentState != null && currentState.GizmoColor != null)
        {
            Gizmos.color = currentState.GizmoColor;
            SetViewGizmoColor(currentState.GizmoColor);
        }
        currentState.DrawGizmos();
    }
}
