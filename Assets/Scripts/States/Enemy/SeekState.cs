using UnityEngine;

public class SeekState : IState
{
    private string name;
    private Color gizmoColor = Color.yellow;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    private EnemyAIController controller;
    private float seekTime = 3f;
    private float seekTimer;

    public SeekState(EnemyAIController anEnemyAIController)
    {
        controller = anEnemyAIController;
        controller.chaseMark.TurnOff();
        controller.questionMark.TurnOn();
    }

    public void Enter()
    {
        Debug.Log(controller.actor.actorTag + " Enter Seek State");
    }

    public void Update()
    {
        controller.Seek();
        seekTimer += Time.deltaTime;
        CheckTransition();
    }

    public void Exit()
    {
        Debug.Log(controller.actor.actorTag + " Exit Seek State");
        controller.questionMark.TurnOff();
    }


    public void CheckTransition()
    {
        if (controller.IsTargetInSight())
        {
            controller.TransitionToState(new ChaseState(controller, controller.GetTargetTransform()));
        }

        if (seekTimer >= seekTime)
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
        foreach(Vector3 v in controller.targetFootprints)
        {
            Gizmos.DrawWireSphere(v, 0.5f);
		}
    }
}
