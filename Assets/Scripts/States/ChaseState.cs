using UnityEngine;

public class ChaseState : IState 
{
    private string name;
    private Color gizmoColor = Color.red;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    public Transform target;

    public ChaseState(Transform aTarget)
    {
        target = aTarget;
	}

    public void Enter(StateController controller)
    {
        Debug.Log(controller.actor.actorName + " Enter Chase State");
        controller.actor.ChangeToChaseView();
	}

    public void Update(StateController controller)
    {
        controller.actor.Chase(target);
        CheckTransition(controller);
	}

    public void Exit(StateController controller)
    { 
        Debug.Log(controller.actor.actorName + " Exit Chase State");
	}

    
    public void CheckTransition(StateController controller)
    { 
        if (!controller.actor.IsTargetInSight())
        {
            controller.TransitionToState(new SeekState());
		}
        if (controller.actor.IsTargetInAttackRange())
        {
            controller.TransitionToState(new AttackState());
		}
	}

    public void DrawGizmos(StateController controller)
    {
        Gizmos.color = Color.white;
        Vector2 dir = (controller.actor.GetTargetTransform().position - controller.actor.transform.position).normalized;
        Gizmos.DrawRay(controller.transform.position, dir * controller.actor.attackRange);
	}
}
