using UnityEngine;

public class AttackState : IState 
{
    private string name;
    private Color gizmoColor = Color.red;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    private StateController controller;
    
    public AttackState(EnemyAIController anEnemyAIController)
    {
        controller = anEnemyAIController;
	}

    // Player can also use this state
    public AttackState(PlayerStateController aPlayerStateController)
    {
        controller = aPlayerStateController;
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
        if (controller.actor.isControlledByAI)
        {
            var enemyAIController = (EnemyAIController)controller;
            if (!enemyAIController.IsTargetInAttackRange())
            {
                controller.TransitionToState(new ChaseState(enemyAIController, enemyAIController.GetTargetTransform()));
            }
            if (!enemyAIController.IsTargetInSight())
            {
                controller.TransitionToState(new SeekState(enemyAIController));
            }
            if (enemyAIController.IsActorDead())
            {
                controller.TransitionToState(new DeadState(enemyAIController));
			}
        }
        else
        { 
            var playerStateController = (PlayerStateController)controller;
            if (playerStateController.IsActorDead())
            {
                controller.TransitionToState(new DeadState(playerStateController));
			}
		}
    }

    public void DrawGizmos()
    {
        if (controller.actor.isControlledByAI)
        {
            Gizmos.color = Color.red;
            Vector2 dir = (((EnemyAIController)controller).GetTargetTransform().position - controller.transform.position).normalized;
            Gizmos.DrawRay(controller.transform.position, dir * controller.actor.attackRange);
        }
    }
}
