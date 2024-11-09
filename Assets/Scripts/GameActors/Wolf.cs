using UnityEngine;

public class Wolf : GameActor
{
    protected void Awake()
    {
        actorName = "Wolf";
        moveSpeed = 6f;
        damage = 3f;
        attackRange = 2f;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
    }
}
