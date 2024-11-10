using UnityEngine;

public enum ActorType
{
    MainCharactor,
    Wolf,
    Fatbat,
}


[CreateAssetMenu(menuName = "GameActor")]
public class GameActor : ScriptableObject
{
    [Header("GameActor Variables")]
    public string actorName;
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public bool canFlay;

    [HideInInspector] public bool isControlledByAI; // Setup by EnemyAIController
    [HideInInspector] public StateController controller;
}
