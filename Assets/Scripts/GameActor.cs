using UnityEngine;

public abstract class GameActor : MonoBehaviour
{
    public string actorName;
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public float attackCD;
    protected float attackCDTimer;
    [HideInInspector] public float patrolViewRangeX;
    [HideInInspector] public float chaseViewRangeX;

    private FieldOfView view;
    private Rigidbody2D body;
    private Animator anim;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        view = GetComponentInChildren<FieldOfView>();
        anim = GetComponent<Animator>();
        patrolViewRangeX = view.sizeX;
        chaseViewRangeX = view.sizeX * 2;
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
    public abstract void Patrol();
    public abstract void Chase(Transform aTarget);
    public abstract void Seek();
}
