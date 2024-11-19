using UnityEngine;

public class CheckpointRespawn : MonoBehaviour
{
    public PlayerController player;
    PlayerCheckpoint checkpoint;

    void Start()
    {
        checkpoint = player.GetComponent<PlayerCheckpoint>();
    }

    public void Respawn()
    {
        player.alive = true;
        player.fell = false;
        checkpoint.Respawn();
    }
}
