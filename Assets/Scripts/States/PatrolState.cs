using UnityEngine;

public class PatrolState : IState 
{
    private string name;
    private Color gizmoColor = Color.green;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    public void Enter(StateController controller)
    {
        Debug.Log(controller.actor.actorName + " Enter Patrol State");
        controller.actor.ChangeToPatrolView();
	}

    public void Update(StateController controller)
    {
        controller.actor.Patrol();
        CheckTransition(controller);
	}

    public void Exit(StateController controller)
    { 
        Debug.Log(controller.actor.actorName + " Exit Patrol State");
	}
    
    public void CheckTransition(StateController controller)
    { 
        if (controller.actor.IsTargetInSight())
        {
            controller.TransitionToState(new ChaseState(controller.actor.GetTargetTransform()));
		}
	}

    public void DrawGizmos(StateController controller)
    { 
	}
}
