using UnityEngine;

public abstract class StateController : MonoBehaviour
{
    public GameActor actor;
    protected IState currentState;
    protected Rigidbody2D body;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        actor = GetComponent<GameActor>();

        actor.controller = this;
    }

    protected virtual void Start()
    {
        currentState?.Enter();
    }

    protected virtual void Update()
    {
        currentState?.Update();
    }

    public void TransitionToState(IState nextState)
    {
        currentState?.Exit();
        currentState = nextState;
        currentState?.Enter();
	}

}
