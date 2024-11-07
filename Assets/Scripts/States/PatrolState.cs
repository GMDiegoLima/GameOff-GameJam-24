using UnityEngine;

public class PatrolState : IState 
{
    private string name;
    private Color gizmoColor = Color.green;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    private EnemyAIController controller;

    public PatrolState(EnemyAIController anEnemyAiController)
    {
        controller = anEnemyAiController;
	}
    
    public void Enter()
    {
        Debug.Log(controller.actor.actorName + " Enter Patrol State");
        controller.ChangeToPatrolView();
	}

    public void Update()
    {
        controller.actor.Patrol();
        CheckTransition();
	}

    public void Exit()
    { 
        Debug.Log(controller.actor.actorName + " Exit Patrol State");
	}
    
    public void CheckTransition()
    { 
        if (controller.IsTargetInSight())
        {
            controller.TransitionToState(new ChaseState(controller, controller.GetTargetTransform()));
		}
	}

    public void DrawGizmos()
    { 
	}
}
