using UnityEngine;

public class MainCharactor : GameActor 
{
    protected override void Awake()
    {
        base.Awake();
        actorName = "MainCharactor";
        moveSpeed = 5f;
        damage = 2f;
        attackRange = 2f;
        attackCD = 1f;
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
}
