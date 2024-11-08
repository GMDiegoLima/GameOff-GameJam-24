using UnityEngine;
using System.Collections;

public class EnemyAIController : StateController 
{
    private FieldOfView view;
    public float velocity;
    [SerializeField] Transform[] patrolPoints;

    protected override void Awake()
    {
        base.Awake();
        view = GetComponentInChildren<FieldOfView>();
    }

    protected override void Start()
    {
        actor.isControlledByAI = true;
        actor.patrolViewRangeX = view.sizeX;
        actor.patrolViewRangeY = view.sizeY;
        actor.chaseViewRangeX = view.sizeX * 2;
        actor.chaseViewRangeY = view.sizeY * 2;
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
        view.sizeY = actor.patrolViewRangeY;
	}

    public void ChangeToChaseView()
    {
        view.sizeX = actor.chaseViewRangeX;
        view.sizeY = actor.chaseViewRangeY;
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
