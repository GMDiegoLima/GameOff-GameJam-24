using UnityEngine;

public class SeekState : IState
{
    private string name;
    private Color gizmoColor = Color.yellow;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    private EnemyAIController controller;
    private float seekTime = 3f;
    private float timer;

    public SeekState(EnemyAIController anEnemyAIController)
    {
        controller = anEnemyAIController;
    }

    public void Enter()
    {
        Debug.Log(controller.actor.actorName + " Enter Seek State");
    }

    public void Update()
    {
        controller.Seek();
        timer += Time.deltaTime;
        CheckTransition();
    }

    public void Exit()
    {
        Debug.Log(controller.actor.actorName + " Exit Seek State");
    }


    public void CheckTransition()
    {
        if (controller.IsTargetInSight())
        {
            controller.TransitionToState(new ChaseState(controller, controller.GetTargetTransform()));
        }
        else if (timer >= seekTime)
        {
            controller.TransitionToState(new PatrolState(controller));
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
