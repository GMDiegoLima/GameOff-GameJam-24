using UnityEngine;

public class ChaseState : IState 
{
    private string name;
    private Color gizmoColor = Color.red;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }
    
    private EnemyAIController controller;
    public Transform target;

    public ChaseState(EnemyAIController anEnemyAIController, Transform aTarget)
    {
        controller = anEnemyAIController;
        target = aTarget;
	}

    public void Enter()
    {
        Debug.Log(controller.actor.actorName + " Enter Chase State");
        controller.ChangeToChaseView();
	}

    public void Update()
    {
        controller.actor.Chase(target);
        CheckTransition();
	}

    public void Exit()
    { 
        Debug.Log(controller.actor.actorName + " Exit Chase State");
	}

    
    public void CheckTransition()
    { 
        if (!controller.IsTargetInSight())
        {
            controller.TransitionToState(new SeekState(controller));
		}
        if (controller.IsTargetInAttackRange())
        {
            controller.TransitionToState(new AttackState(controller));
		}
	}

    public void DrawGizmos()
    {
        Gizmos.color = Color.white;
        Vector2 dir = (controller.GetTargetTransform().position - controller.transform.position).normalized;
        Gizmos.DrawRay(controller.transform.position, dir * controller.actor.attackRange);
	}
}
