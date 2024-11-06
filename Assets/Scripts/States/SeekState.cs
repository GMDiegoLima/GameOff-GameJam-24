using UnityEngine;

public class SeekState : IState 
{
    private string name;
    private Color gizmoColor = Color.yellow;
    public string Name { get => name; set => name = value; }
    public Color GizmoColor { get => gizmoColor; set => gizmoColor = value; }

    private float seekTime = 3f;
    private float timer;

    public void Enter(StateController controller)
    {
        Debug.Log(controller.actor.actorName + " Enter Seek State");
	}

    public void Update(StateController controller)
    {
        controller.actor.Seek();
        timer += Time.deltaTime;
        CheckTransition(controller);
	}

    public void Exit(StateController controller)
    { 
        Debug.Log(controller.actor.actorName + " Exit Seek State");
	}

    
    public void CheckTransition(StateController controller)
    { 
        if (controller.actor.IsTargetInSight())
        {
            controller.TransitionToState(new ChaseState(controller.actor.GetTargetTransform()));
		}
        else if (timer >= seekTime)
        { 
            controller.TransitionToState(new PatrolState());
		}    
	}

    public void DrawGizmos(StateController controller)
    { 
	}
}
