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
        Debug.Log(controller.actor.actorName + " Enter Attack State");
	}

    public void Update()
    {
        controller.actor.Attack();
        CheckTransition();
	}

    public void Exit()
    { 
        Debug.Log(controller.actor.actorName + " Exit Attack State");
	}

    
    public void CheckTransition()
    { 
        if (!controller.IsTargetInAttackRange())
        {
            controller.TransitionToState(new ChaseState(controller, controller.GetTargetTransform()));
		}
	}

    public void DrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 dir = (controller.GetTargetTransform().position - controller.actor.transform.position).normalized;
        Gizmos.DrawRay(controller.transform.position, dir * controller.actor.attackRange);
	}
}
