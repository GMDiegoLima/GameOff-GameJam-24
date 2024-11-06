using UnityEngine;

public class AttackState : IState 
{
    private string name;
    private Color gizmoColor = Color.red;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    public void Enter(StateController controller)
    {
        Debug.Log(controller.actor.actorName + " Enter Attack State");
	}

    public void Update(StateController controller)
    {
        controller.actor.Attack();
        CheckTransition(controller);
	}

    public void Exit(StateController controller)
    { 
        Debug.Log(controller.actor.actorName + " Exit Attack State");
	}

    
    public void CheckTransition(StateController controller)
    { 
        if (!controller.actor.IsTargetInAttackRange())
        {
            controller.TransitionToState(new ChaseState(controller.actor.GetTargetTransform()));
		}
	}

    public void DrawGizmos(StateController controller)
    {
        Gizmos.color = Color.red;
        Vector2 dir = (controller.actor.GetTargetTransform().position - controller.actor.transform.position).normalized;
        Gizmos.DrawRay(controller.transform.position, dir * controller.actor.attackRange);
	}
}
