using UnityEngine;

public class Enemy : GameActor 
{
    protected override void Awake()
    {
        base.Awake();
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
