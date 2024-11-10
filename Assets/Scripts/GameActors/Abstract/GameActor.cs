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

    [HideInInspector] public bool isControlledByAI; // Setup by EnemyAIController
    [HideInInspector] public StateController controller;

    protected virtual void Update()
    {
    }

    public virtual void Attack() { }
}
