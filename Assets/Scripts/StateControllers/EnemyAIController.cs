using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyAIController : StateController
{
    public Vector2 velocity;
    [SerializeField] private LayerMask enemyViewLayer;
    [SerializeField] private GameObject bonePrefab;

    [Header("Adjustable")]
    [SerializeField] private float minMovePeriod;
    [SerializeField] private float maxMovePeriod;
    [SerializeField] private float minStopPeriod;
    [SerializeField] private float maxStopPeriod;
    [SerializeField] private float minVelocity;
    [SerializeField] private float maxVelocity;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float attackCD;

    [Header("For Dubug")]
    [SerializeField] private bool isMoving;
    [SerializeField] private float movePeriodTimer;
    [SerializeField] private float stopPeriodTimer;
    [SerializeField] private float attackCDTimer;

    private FieldOfView view;
    private CapsuleCollider2D bodyCollider;
    [HideInInspector] public ChaseMark chaseMark;
    [HideInInspector] public QuestionMark questionMark;

    private float movePeriod = 2f;
    private float stopPeriod = 2f;
    private bool isVelocityUpdated;
    private bool isMovePeriodUpdated;
    private bool isStopPeriodUpdated;

    public LinkedList<Vector3> targetFootprints = new(); // for chasing
    private Vector3 nextFootprint = Vector3.positiveInfinity;

    protected override void Awake()
    {
        base.Awake();
        view = GetComponentInChildren<FieldOfView>(); // Get from Eye
        bodyCollider = GetComponentInChildren<CapsuleCollider2D>(); // Get from BodyCollider
        chaseMark = GetComponentInChildren<ChaseMark>();
        questionMark = GetComponentInChildren<QuestionMark>();
        actor.isControlledByAI = true;
        currentState = new PatrolState(this);
        currentState.Enter();
    }

    protected override void Start()
    {
        UpdateScanAngle();
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
        Vector2 dir = new Vector2(anim.GetFloat("LastHorizontal"), anim.GetFloat("LastVertical")).normalized;
        HandleActorAttack(dir);
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
        // Record the target's footsprints and follow it
        if (!isMoving)
        {
            isMoving = true;
            stopPeriodTimer = 0f;
        }

        if (targetFootprints.Count == 0)
        {
            targetFootprints.AddLast(aTarget.position);
		}
        else if ((aTarget.position - targetFootprints.Last.Value).magnitude > 1f)
        {
            // Don't add the footsprint if too close to the last one
            targetFootprints.AddLast(aTarget.position);
        }

        nextFootprint = targetFootprints.First.Value;
        velocity = chaseSpeed * (nextFootprint - transform.position).normalized;

        if ((transform.position - nextFootprint).magnitude < 0.1f && targetFootprints.Count != 0)
        {
            targetFootprints.RemoveFirst();
        }

        Vector3 playerDir = aTarget.position - transform.position;
        view.transform.rotation = Quaternion.Euler(playerDir);
    }

    public void Seek() 
	{
        // Follow the remaining footprints
        if (!isMoving) isMoving = true;
        if (targetFootprints.Count != 0)
        {
            nextFootprint = targetFootprints.First.Value;
            velocity = chaseSpeed * (nextFootprint - transform.position).normalized;
            if ((transform.position - nextFootprint).magnitude < 0.1f)
            {
                targetFootprints.RemoveFirst();
            }
        }
        else
        {
            velocity = Vector2.zero;
        }
    }

    public override void Dead()
    {
        chaseMark.TurnOff();
        questionMark.TurnOff();
        anim.SetBool("Dead", true);
        gameObject.tag = "DeadBody";
        velocity = Vector2.zero;
        view.enabled = false;
        bodyCollider.enabled = false;
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
        // It's actually changing the rotation of Eye transform
        // The Raycast always cast up
        float x = velocity.x;
        float y = velocity.y;
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        if (angle > -45 && angle < 45) // facing right
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (angle > -135 && angle < -45) // facing down
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (angle < -135 || angle > 135) // facing left
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else // facing up
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        view.right45 = (view.transform.up + view.transform.right).normalized;
        view.left45 = (view.transform.up - view.transform.right).normalized;
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
        view.scanDistance = view.patrolViewDistance;
        view.isChasing = false;
    }

    public void ChangeToChaseView()
    {
        view.scanDistance = view.chaseViewDistance;
        view.isChasing = true;
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

    private void HandleActorAttack(Vector2 aDir)
    { 
        // Attack based on current actor type
        if (actor.actorType == ActorType.Skeleton)
        {
            ThrowBone((view.targetTransform.position - transform.position).normalized);
            AkSoundEngine.PostEvent("skeleton_attack", gameObject);
            return;
		}

        // Play SFX
        if (actor.actorType == ActorType.Wolf) AkSoundEngine.PostEvent("wolf_attack", gameObject);
        else if (actor.actorType == ActorType.Fatbat) AkSoundEngine.PostEvent("bat_attack", gameObject);
        else if (actor.actorType == ActorType.Goblin) AkSoundEngine.PostEvent("goblin_attack", gameObject);

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0,
                                            aDir, actor.attackRange, enemyViewLayer);
        if (hit)
        {
            if (hit.transform.TryGetComponent<Health>(out Health playerHealth))
            {
                Debug.Log("Got hit by enemy");
                playerHealth.TakeDamage(actor.damage);
            }
        }
	}

    private void ThrowBone(Vector2 dir)
    {
        Debug.Log("Throw bone");
        GameObject theBone = Instantiate(bonePrefab, transform.position + (Vector3)dir, Quaternion.identity);
        theBone.GetComponent<Rigidbody2D>().AddForce(dir * 6f, ForceMode2D.Impulse);
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
