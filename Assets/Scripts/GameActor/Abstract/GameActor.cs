using UnityEngine;

public enum ActorType
{ 
    MainCharactor,
    Wolf,
    Fatbat,
}

public abstract class GameActor : MonoBehaviour
{
    [Header("GameActor Variables")]
    public string actorName;
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public float attackCD;
    protected float attackCDTimer;

    [HideInInspector] public float patrolViewRangeX;
    [HideInInspector] public float chaseViewRangeX;
    [HideInInspector] public bool isControllerEnabled;

    private FieldOfView view;
    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer sprite;
    private StateController controller;
    public FieldOfView View { get => view; private set => view = value; }
    public Rigidbody2D Body { get => body; private set => body = value; }
    public Animator Anim { get => anim; private set => anim = value; }
    public SpriteRenderer Sprite { get => sprite; private set => sprite  = value; }

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (isControllerEnabled)
        {
            view = GetComponentInChildren<FieldOfView>();
            patrolViewRangeX = view.sizeX;
            chaseViewRangeX = view.sizeX * 2;
        }
    }

    protected virtual void Update()
    {
        attackCDTimer += Time.deltaTime;
    }

    public virtual void MoveTo(Vector2 aPosition)
    { 
	    
	}

    public bool IsTargetInSight()
    {
        return view.isTargetInSight;
	}

    public Transform GetTargetTransform()
    {
        return view.targetTransform;
	}

    public void ChangeToPatrolView()
    {
        view.sizeX = patrolViewRangeX;
	}

    public void ChangeToChaseView()
    {
        view.sizeX = chaseViewRangeX;
	}

    public void SetViewGizmoColor(Color aColor)
    {
        view.gizmoColor = aColor;
	}

    public bool IsTargetInAttackRange()
    { 
        if (GetTargetTransform() != null && (transform.position - GetTargetTransform().position).magnitude <= attackRange)
        {
            return true;
		}
        return false;
	}

    public abstract void Attack();
    public virtual void Patrol() {}
    public virtual void Chase(Transform aTarget) {}
    public virtual void Seek() {}
}
