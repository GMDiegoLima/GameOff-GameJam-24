using UnityEngine;

public class AliveState : IState 
{
    private string name;
    private Color gizmoColor = Color.red;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    private PlayerStateController controller;
    public Transform target;

    public AliveState(PlayerStateController aController)
    {
        controller = aController;
    }

    public void Enter()
    {
        Debug.Log(controller.actor.actorTag + " Enter Alive State");
    }

    public void Update()
    {
        controller.Alive();
        CheckTransition();
    }

    public void Exit()
    {
        Debug.Log(controller.actor.actorTag + " Exit Alive State");
    }


    public void CheckTransition()
    {
        if (controller.IsActorDead())
        {
            controller.TransitionToState(new DeadState(controller));
        }
    }

    public void DrawGizmos()
    {
    }
}
