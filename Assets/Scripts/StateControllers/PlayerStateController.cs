using UnityEngine;

public class PlayerStateController : StateController
{
    [SerializeField] KeyCode attackKey;
    [SerializeField] float attackCD;

    [Header("For Debug")]
    [SerializeField] float attackCDTimer;

    protected override void Update()
    {
        base.Update();
        attackCDTimer += Time.deltaTime;
        if (Input.GetKeyDown(attackKey) && attackCDTimer >= attackCD)
        {
            Attack();
        }
    }

    public void RemoveCurrentActor()
    {
        Destroy(actor);
    }

    public void UpdateCurrentActor(GameActor anActor)
    {
        actor = anActor;
        actor.controller = this;
    }

    public override void Attack()
    {
        base.Attack(); // actor.Attack()
        Debug.Log(actor.actorName + "::Attack()");
        anim.Play("Attack");
        attackCDTimer = 0;
    }
}
