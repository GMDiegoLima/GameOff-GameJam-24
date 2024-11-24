using UnityEngine;

public class CheckpointRespawn : MonoBehaviour
{
    public PlayerController player;
    Health playerHealth;
    PlayerCheckpoint checkpoint;

    void Start()
    {
        checkpoint = player.GetComponent<PlayerCheckpoint>();
        playerHealth = player.GetComponent<Health>();
    }

    public void Respawn()
    {
        player.alive = true;
        player.fell = false;
        playerHealth.currentHealth = 3;
        checkpoint.Respawn();
    }
}
