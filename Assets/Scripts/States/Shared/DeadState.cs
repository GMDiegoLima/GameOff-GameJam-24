using UnityEngine;

public class DeadState : IState 
{
    private string name;
    private Color gizmoColor = Color.red;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    private StateController controller;

    public DeadState(EnemyAIController anEnemyAIController)
    {
        controller = anEnemyAIController;
    }

    public DeadState(PlayerStateController aPlayerStateController)
    {
        controller = aPlayerStateController;
	}

    public void Enter()
    {
        Debug.Log(controller.actor.actorTag + " Enter Dead State");
        controller.Dead();
    }

    public void Update()
    {
        //CheckTransition();
    }

    public void Exit()
    {
        Debug.Log(controller.actor.actorTag + " Exit Dead State");
    }


    public void CheckTransition()
    {
        // No transition to other state after dead
    }

    public void DrawGizmos()
    {
    }
}
