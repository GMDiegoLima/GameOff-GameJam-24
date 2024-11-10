public class Goblin : GameActor 
{
    protected void Awake()
    {
        actorName = "Goblin";
        moveSpeed = 5f;
        damage = 2f;
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
