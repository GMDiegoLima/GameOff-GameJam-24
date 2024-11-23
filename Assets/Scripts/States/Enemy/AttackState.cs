using UnityEngine;

public class AttackState : IState 
{
    private string name;
    private Color gizmoColor = Color.red;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    private EnemyAIController controller;
    
    public AttackState(EnemyAIController anEnemyAIController)
    {
        controller = anEnemyAIController;
	}

    public void Enter()
    {
        Debug.Log(controller.actor.actorTag + " Enter Attack State");
	}

    public void Update()
    {
        controller.Attack();
        CheckTransition();
	}

    public void Exit()
    { 
        Debug.Log(controller.actor.actorTag + " Exit Attack State");
	}

    
    public void CheckTransition()
    {
        if (!controller.IsTargetInAttackRange())
        {
            controller.TransitionToState(new ChaseState(controller, controller.GetTargetTransform()));
        }
        if (!controller.IsTargetInSight())
        {
            controller.TransitionToState(new SeekState(controller));
        }
        if (controller.IsActorDead())
        {
            controller.TransitionToState(new DeadState(controller));
        }
}

    public void DrawGizmos()
    {
        if (controller.actor.isControlledByAI)
        {
            Gizmos.color = Color.red;
            Vector2 dir = (controller.GetTargetTransform().position - controller.transform.position).normalized;
            Gizmos.DrawRay(controller.transform.position, dir * controller.actor.attackRange);
        }
    }
}
