using UnityEngine;

public class StateController : MonoBehaviour
{
    public GameActor actor;
    private IState currentState;

    private void Awake()
    {
        actor = GetComponent<GameActor>();
        currentState = new PatrolState();
    }

    private void Start()
    {
        currentState.Enter(this);       
    }

    private void Update()
    {
        currentState.Update(this);
    }

    public void TransitionToState(IState nextState)
    {
        currentState.Exit(this);
        currentState = nextState;
        currentState.Enter(this);
	}

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        if (currentState != null && currentState.GizmoColor != null)
        {
            Gizmos.color = currentState.GizmoColor;
            actor.SetViewGizmoColor(currentState.GizmoColor);
        }
        currentState.DrawGizmos(this);
    }
}
