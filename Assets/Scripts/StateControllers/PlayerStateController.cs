using UnityEngine;
using System.Collections;

public class PlayerStateController : StateController
{
    [SerializeField] KeyCode attackKey;
    [SerializeField] float attackCD;
    [SerializeField] LayerMask attackTargetLayer;
    [SerializeField] GameObject bonePrefab; // For Skeleton

    [Header("For FatBat")]
    [SerializeField] float blowForce;

    [Header("For Skeleton")]
    [SerializeField] float throwForce;

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
        ActorType currentActorType = actor.actorType;
        if (currentActorType == ActorType.MainCharactor || currentActorType == ActorType.Ghost) return;

        Debug.Log(actor.actorTag + "::Attack()");
        anim.Play("Attack");
        // Play SFX
        if (actor.actorType == ActorType.Wolf) AkSoundEngine.PostEvent("wolf_attack", gameObject);
        else if (actor.actorType == ActorType.Fatbat) AkSoundEngine.PostEvent("bat_attack", gameObject);
        else if (actor.actorType == ActorType.Goblin) AkSoundEngine.PostEvent("goblin_attack", gameObject);

        // Attack based on current actor type
        Vector2 dir = new Vector2(anim.GetFloat("LastHorizontal"), anim.GetFloat("LastVertical")).normalized;
        if (actor.actorType == ActorType.Skeleton)
        {
            ThrowBone(dir);
            AkSoundEngine.PostEvent("skeleton_attack", gameObject);
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
            else if (actor.actorType == ActorType.Fatbat)
            { 
                Debug.Log("Blow something");
                if (hit.transform.TryGetComponent<Rigidbody2D>(out Rigidbody2D aBody))
                {
                    aBody.AddForce(dir * blowForce, ForceMode2D.Impulse);
				}
			}
            else if (actor.actorType == ActorType.Wolf && hit.transform.TryGetComponent<IBiteable>(out IBiteable aBiteable))
            {
                Debug.Log("Bit something");
                aBiteable.GetBit();
            }
            else if (actor.actorType == ActorType.Goblin && hit.transform.TryGetComponent<ICuttable>(out ICuttable aCuttable))
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

    private void ThrowBone(Vector2 dir)
    {
        Debug.Log("Throw bone");
        GameObject theBone = Instantiate(bonePrefab, transform.position + (Vector3)dir, Quaternion.identity);
        theBone.GetComponent<Rigidbody2D>().AddForce(dir * throwForce, ForceMode2D.Impulse);
	}

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;
        Vector2 dir = new Vector2(anim.GetFloat("LastHorizontal"), anim.GetFloat("LastVertical")).normalized;
        Gizmos.DrawRay(transform.position, dir * (actor.attackRange + 0.5f)); // Attack range
    }
}
