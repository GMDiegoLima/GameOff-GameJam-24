using UnityEngine;

public class EnemyAIController : StateController
{
    public Vector2 velocity;

    [Header("Adjustable")]
    [SerializeField] private float minMovePeriod;
    [SerializeField] private float maxMovePeriod;
    [SerializeField] private float minStopPeriod;
    [SerializeField] private float maxStopPeriod;
    [SerializeField] private float attackCD;

    [Header("For Dubug")]
    [SerializeField] private bool isMoving;
    [SerializeField] private float movePeriodTimer;
    [SerializeField] private float stopPeriodTimer;
    [SerializeField] private float attackCDTimer;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float minVelocity;
    [SerializeField] private float maxVelocity;

    private FieldOfView view;

    private float movePeriod = 2f;
    private float stopPeriod = 2f;
    private bool isVelocityUpdated;
    private bool isMovePeriodUpdated;
    private bool isStopPeriodUpdated;

    protected override void Awake()
    {
        base.Awake();
        chaseSpeed = actor.moveSpeed;
        minVelocity = -actor.moveSpeed;
        maxVelocity = actor.moveSpeed;
        view = GetComponentInChildren<FieldOfView>(); // Get from Eye
    }

    protected override void Start()
    {
        actor.isControlledByAI = true;
        currentState = new PatrolState(this);
    }

    private void FixedUpdate()
    {
        Vector2 delta = velocity * Time.deltaTime;
        Vector2 newPosition = body.position + delta;
        body.MovePosition(newPosition);
    }

    protected override void Update()
    {
        base.Update();
        HandleAnim();
        attackCDTimer += Time.deltaTime;
    }

    // -------------- States --------------
    public override void Attack()
    {
        velocity = Vector2.zero;
        if (attackCDTimer < attackCD) return;
        anim.Play("Attack");
        attackCDTimer = 0;
    }

    public void Patrol()
    {
        // Randomly set move period, stop period, and velocity
        if (isMoving)
        {
            movePeriodTimer += Time.deltaTime;
            if (!isVelocityUpdated)
            {
                SetRandomVelocity();
                isVelocityUpdated = true;
            }
            if (!isMovePeriodUpdated)
            {
                SetRandomMovePeriod();
                isMovePeriodUpdated = true;
            }
            if (movePeriodTimer >= movePeriod)
            {
                isMoving = false;
                isVelocityUpdated = false;
                isMovePeriodUpdated = false;
                movePeriodTimer = 0;
            }
        }
        else
        {
            stopPeriodTimer += Time.deltaTime;
            velocity = Vector2.zero;
            if (!isStopPeriodUpdated)
            {
                SetRandomStopPeriod();
                isStopPeriodUpdated = true;
            }
            if (stopPeriodTimer >= stopPeriod)
            {
                isMoving = true;
                isStopPeriodUpdated = false;
                stopPeriodTimer = 0;
            }
        }
    }

    public void Chase(Transform aTarget)
    {
        if (!isMoving) isMoving = true;
        velocity = chaseSpeed * (aTarget.position - transform.position).normalized;
        view.transform.localPosition = Vector2.zero; // set the boxcast to be in the center and cast outward
    }

    public void Seek() 
	{
	    // Now seek is just to keep the same velocity for 3 sec
	}

    public void Dead()
    {
        transform.tag = "DeadBody";
        anim.SetBool("Dead", true);
        this.enabled = false;
	}

    // -------------- States --------------

    private void HandleAnim()
    { 
        anim.SetFloat("Horizontal", velocity.x);
        anim.SetFloat("Vertical", velocity.y);

        if (velocity != Vector2.zero)
        {
            anim.SetFloat("LastHorizontal", velocity.x);
            anim.SetFloat("LastVertical", velocity.y);
        }
	}

    private void SetRandomVelocity()
    {
        float x = Random.Range(minVelocity, maxVelocity);
        float y = Random.Range(minVelocity, maxVelocity);
        movePeriodTimer = 0;
        velocity = new Vector2(x, y);
        UpdateScanAngle();
    }

    private void SetRandomMovePeriod()
    {
        movePeriod = Random.Range(minMovePeriod, maxMovePeriod);
    }

    private void SetRandomStopPeriod()
    {
        stopPeriod = Random.Range(minStopPeriod, maxStopPeriod);
    }

    private void UpdateScanAngle()
    {
        // It's actually changing the position and rotation of Eye transform
        // The boxcast is always cast up
        float x = velocity.x;
        float y = velocity.y;
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        if (angle > -45 && angle < 45)
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 180);
            view.transform.localPosition = new Vector2(view.sizeX * 0.5f, 0);
        }
        else if (angle > -135 && angle < -45)
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 180);
            view.transform.localPosition = new Vector2(0, 0);
        }
        else if (angle < -135 || angle > 135)
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 180);
            view.transform.localPosition = new Vector2(-view.sizeX * 0.5f, 0);
        }
        else
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 0);
            view.transform.localPosition = new Vector2(0, 0);
        }

    }

    public bool IsActorDead()
    {
        return health.currentHealth <= 0;
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
        view.sizeX = view.patrolViewSizeX;
        view.sizeY = view.patrolViewSizeY;
        view.scanDistance = view.patrolViewDistance;
    }

    public void ChangeToChaseView()
    {
        view.sizeX = view.chaseViewSizeX;
        view.sizeY = view.chaseViewSizeY;
        view.scanDistance = 0;
    }

    public void SetViewGizmoColor(Color aColor)
    {
        view.gizmoColor = aColor;
    }

    public bool IsTargetInAttackRange()
    {
        if (GetTargetTransform() != null && (transform.position - GetTargetTransform().position).magnitude <= actor.attackRange)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.red;
        if (currentState != null && currentState.GizmoColor != null)
        {
            Gizmos.color = currentState.GizmoColor;
            SetViewGizmoColor(currentState.GizmoColor);
        }
        currentState.DrawGizmos();
    }
}
