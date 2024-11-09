using UnityEngine;

public class MainCharacter : GameActor
{
    protected void Awake()
    {
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

    public override void Attack()
    {
        attackCDTimer = 0;
    }
}
