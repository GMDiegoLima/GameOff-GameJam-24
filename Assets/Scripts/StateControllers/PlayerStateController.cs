using UnityEngine;
using System.Collections;

public class PlayerStateController : StateController
{
    [SerializeField] KeyCode attackKey;
    [SerializeField] float attackCD;
    [SerializeField] LayerMask attackTargetLayer;
    [SerializeField] GameObject bonePrefab;

    [Header("For Debug")]
    [SerializeField] float attackCDTimer;

    private PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
    }

    protected override void Start()
    {
        UpdateActorController();
        currentState = new AliveState(this);
        currentState.Enter();
    }

    protected override void Update()
    {
        base.Update();

        attackCDTimer = Mathf.Clamp(attackCDTimer + Time.deltaTime, 0f, attackCD);
        HandleAttack();
    }

    public void RemoveCurrentActor()
    {
        Destroy(actor);
    }

    public void HandleAttack()
    { 
        if (Input.GetKeyDown(attackKey) && attackCDTimer >= attackCD)
        {
            Attack();
            attackCDTimer = 0;
        }
    }

    public override void Attack()
    {
        Debug.Log(actor.actorTag + "::Attack()");
        anim.Play("Attack");
        Vector2 dir = new Vector2(anim.GetFloat("LastHorizontal"), anim.GetFloat("LastVertical")).normalized;

        if (actor.actorType == ActorType.Skeleton)
        {
            ThrowBone(dir);
            return;
		}

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0,
                                            dir, actor.attackRange, attackTargetLayer);
        if (hit)
        {
            Debug.Log("Hit");
            if (hit.transform.TryGetComponent<Health>(out Health enemyHealth))
            {
                Debug.Log("Attack someone");
                enemyHealth.TakeDamage(actor.damage);
            }
            if (actor.actorType == ActorType.Wolf && hit.transform.TryGetComponent<IBiteable>(out IBiteable aBiteable))
            {
                Debug.Log("Bit something");
                aBiteable.GetBit();
            }
            if (actor.actorType == ActorType.Goblin && hit.transform.TryGetComponent<ICuttable>(out ICuttable aCuttable))
            {
                Debug.Log("Cut something");
                aCuttable.GetCut();
            }
        }
    }

    // ------------ State -------------
    public void Alive()
    { 
        // For initial state
	}

    public override void Dead()
    {
        anim.SetBool("Dead", true);
        playerController.alive = false;
    }
    // ------------ State -------------

    public void UpdateActorController()
    {
        actor.isControlledByAI = false;
        actor.controller = this;
    }

    public bool IsActorDead()
    {
        return health.currentHealth <= 0;
	}
    
    private void ThrowBone(Vector2 dir)
    {
        Debug.Log("Throw bone");
        GameObject theBone = Instantiate(bonePrefab, transform.position, Quaternion.identity);
        StartCoroutine(BoneLaunchRoutine(theBone, dir));
	}

    IEnumerator BoneLaunchRoutine(GameObject aBone, Vector2 dir)
    {
        Vector3 bonePos = aBone.transform.position;
        Vector3 endPos = bonePos + (Vector3)dir * 3f;
        while (bonePos != endPos)
        {
            aBone.transform.position = Vector3.MoveTowards(aBone.transform.position, endPos, 3f * Time.deltaTime);
            yield return new WaitWhile(() => aBone.transform.position == endPos);
        }
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;
        Vector2 dir = new Vector2(anim.GetFloat("LastHorizontal"), anim.GetFloat("LastVertical")).normalized;
        Gizmos.DrawRay(transform.position, dir * (actor.attackRange + 0.5f)); // Attack range
    }
}
