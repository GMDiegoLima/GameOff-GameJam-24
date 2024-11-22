using UnityEngine;

public enum ActorType
{
    MainCharactor,
    Ghost,
    Wolf,
    Fatbat,
    Skeleton,
    Goblin
}


[CreateAssetMenu(menuName = "GameActor")]
public class GameActor : ScriptableObject
{
    public string actorTag;
    public ActorType actorType;

    [Header("Player Variables")]
    public float moveSpeed; 
    public bool canFlay;

    [Header("Shared Variables")]
    public float damage;
    public float attackRange;

    [Header("Enemy AI Variables")]
    public float health;

    [HideInInspector] public bool isControlledByAI; // Setup by EnemyAIController
    [HideInInspector] public StateController controller;
}
