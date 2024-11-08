using UnityEngine;

public enum ActorType
{ 
    MainCharactor,
    Wolf,
    Fatbat,
}

public abstract class GameActor : MonoBehaviour
{
    [Header("GameActor Variables")]
    public string actorName;
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public float attackCD;
    public float velocity;
    protected float attackCDTimer;

    // Setup by EnemyAIController
    [HideInInspector] public float patrolViewRangeX;
    [HideInInspector] public float patrolViewRangeY;
    [HideInInspector] public float chaseViewRangeX;
    [HideInInspector] public float chaseViewRangeY;
    [HideInInspector] public bool isControlledByAI;
    [HideInInspector] public StateController controller;

    protected virtual void Update()
    {
        attackCDTimer += Time.deltaTime;
    }

    public virtual void Attack() {}
}
