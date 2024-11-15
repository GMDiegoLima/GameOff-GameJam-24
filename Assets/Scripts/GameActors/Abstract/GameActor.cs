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
    [Header("GameActor Variables")]
    public string actorTag;
    public ActorType actorType;
    public float moveSpeed; // For player when enbody
    public float damage;
    public float attackRange;
    public float health;
    public bool canFlay;


    [HideInInspector] public bool isControlledByAI; // Setup by EnemyAIController
    [HideInInspector] public StateController controller;
}
