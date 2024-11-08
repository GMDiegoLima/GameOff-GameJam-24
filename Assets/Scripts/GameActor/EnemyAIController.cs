using UnityEngine;
using System.Collections;

public class EnemyAIController : StateController 
{
    private FieldOfView view;
    public Vector2 velocity;
    public float chaseSpeed;
    public float movePeriod = 3;
    public float stopPeriod = 3;
    public bool isMoving;
    private bool isVelocityUpdated;
    private bool isMovePeriodUpdated;
    private bool isStopPeriodUpdated;
    [SerializeField] private float movePeriodTimer; 
    [SerializeField] private float stopPeriodTimer; 

    protected override void Awake()
    {
        base.Awake();
        view = GetComponentInChildren<FieldOfView>();
    }

    protected override void Start()
    {
        actor.isControlledByAI = true;
        actor.patrolViewRangeX = view.sizeX;
        actor.patrolViewRangeY = view.sizeY;
        actor.chaseViewRangeX = view.sizeX * 2;
        actor.chaseViewRangeY = view.sizeY * 2;
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

        anim.SetFloat("Horizontal", velocity.x);
        anim.SetFloat("Vertical", velocity.y);

        if (velocity != Vector2.zero)
        {
            anim.SetFloat("LastHorizontal", velocity.x);
            anim.SetFloat("LastVertical", velocity.y);
        }
    }

    public override void Attack() 
	{
        base.Attack();
	}

    public void Patrol() 
	{
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
	}

    public void Seek() {}

    private void SetRandomVelocity()
    {
        float x = Random.Range(-3f, 3f);
        float y = Random.Range(-3f, 3f);
        movePeriodTimer = 0;
        velocity = new Vector2(x, y);
        UpdateScanAngle();
    }

    private void SetRandomMovePeriod()
    {
        movePeriod = Random.Range(1, 3);
	}

    private void SetRandomStopPeriod()
    { 
        stopPeriod = Random.Range(2, 5);
	}

    private void UpdateScanAngle()
    {
        float x = velocity.x;
        float y = velocity.y;
        float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        if (angle > -45 && angle < 45)
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 180);
            view.transform.localPosition= new Vector2(view.sizeX * 0.5f, 0);
		} 
        else if (angle > -135 && angle < -45)
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 180);
            view.transform.localPosition= new Vector2(0, 0);
		}
        else if (angle < -135 || angle > 135)
        {
            view.transform.rotation = Quaternion.Euler(0, 0, 180);
            view.transform.localPosition= new Vector2(-view.sizeX * 0.5f, 0);
		}
        else 
		{
            view.transform.rotation = Quaternion.Euler(0, 0, 0);
            view.transform.localPosition= new Vector2(0, 0);
		}

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
        view.sizeX = actor.patrolViewRangeX;
        view.sizeY = actor.patrolViewRangeY;
	}

    public void ChangeToChaseView()
    {
        view.sizeX = actor.chaseViewRangeX;
        view.sizeY = actor.chaseViewRangeY;
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
