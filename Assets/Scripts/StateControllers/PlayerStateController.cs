using UnityEngine;

public class PlayerStateController : StateController
{
    [SerializeField] KeyCode attackKey;
    [SerializeField] float attackCD;
    [SerializeField] LayerMask attackTargetLayer;

    [Header("For Debug")]
    [SerializeField] float attackCDTimer;

    private PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
    }

    protected override void Update()
    {
        base.Update();
        HandleAttack();
    }

    public void RemoveCurrentActor()
    {
        Destroy(actor);
    }

    public void UpdateActorController()
    {
        actor.isControlledByAI = false;
        actor.controller = this;
    }

    private void HandleAttack()
    { 
        attackCDTimer += Time.deltaTime;
        if (Input.GetKeyDown(attackKey) && attackCDTimer >= attackCD)
        {
            Attack();
        }
	}

    // ------------ State -------------
    public override void Attack()
    {
        Debug.Log(actor.actorTag + "::Attack()");
        anim.Play("Attack");
        Vector2 dir = new Vector2(anim.GetFloat("LastHorizontal"), anim.GetFloat("LastVertical")).normalized;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 1), 0, 
			                                dir, actor.attackRange, attackTargetLayer);
        if (hit)
        {
            Health enemyHealth;  
			if (hit.transform.TryGetComponent<Health>(out enemyHealth))
            {
                enemyHealth.TakeDamage(actor.damage);
                Debug.Log("Hit someone");
            }
        }
        attackCDTimer = 0;
    }

    public override void Dead()
    {
        anim.SetBool("Dead", true);
    }
    // ------------ State -------------

    public bool IsActorDead()
    {
        return health.currentHealth <= 0;
	}

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Gizmos.color = Color.green;
        Vector2 dir = new Vector2(anim.GetFloat("LastHorizontal"), anim.GetFloat("LastVertical")).normalized;
        Gizmos.DrawRay(transform.position, dir * (actor.attackRange + 0.5f));
    }
}
