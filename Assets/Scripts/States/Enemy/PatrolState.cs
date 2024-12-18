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
        Debug.Log(controller.actor.actorTag + " Enter Patrol State");
        controller.ChangeToPatrolView();
    }

    public void Update()
    {
        CheckTransition();
        controller.Patrol();
    }

    public void Exit()
    {
        Debug.Log(controller.actor.actorTag + " Exit Patrol State");
    }

    public void CheckTransition()
    {
        if (controller.IsTargetInSight())
        {
            controller.TransitionToState(new ChaseState(controller, controller.GetTargetTransform()));
        }
        if (controller.IsActorDead())
        {
            controller.TransitionToState(new DeadState(controller));
        }
    }

    public void DrawGizmos()
    {
    }
}
