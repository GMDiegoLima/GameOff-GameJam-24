using UnityEngine;

public class FatBat : GameActor 
{
    protected void Awake()
    {
        actorName = "FatBat";
        moveSpeed = 7f;
        damage = 1f;
        attackRange = 3f;
        attackCD = 2f;
    }

    protected override void Update()
    {
        base.Update();       
    }

    public override void Attack()
    {
        attackCDTimer = 0;
	}
}
