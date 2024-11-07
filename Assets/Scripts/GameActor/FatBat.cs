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

    public override void MoveTo(Vector2 aPosition)
    {
        base.MoveTo(aPosition);
	}

    public override void Attack()
    {
        attackCDTimer = 0;
	}

    public override void Patrol()
    { 
	}

    public override void Chase(Transform aTarget)
    { 
	}

    public override void Seek()
    { 
	}

}
