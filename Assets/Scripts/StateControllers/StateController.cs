using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(SpriteRenderer))]
[RequireComponent(typeof(Health))]
public abstract class StateController : MonoBehaviour
{
    public GameActor actor;
    protected IState currentState;
    protected Rigidbody2D body;
    protected Animator anim;
    protected SpriteRenderer sprite;
    protected Health health;

    [HideInInspector] public bool isPoisoned;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
        health.maxHealth = actor.health;

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

    public abstract void Attack();
    public abstract void Dead();
}
