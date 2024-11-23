using UnityEngine;

// Used by the Checkpoint and CheckpointRespawn to set checkpoint and respawn the player
public class PlayerCheckpoint : MonoBehaviour
{
    Vector3 lastCheckpoint;

    public void SetCheckpoint(Vector3 checkpointPosition)
    {
        lastCheckpoint = checkpointPosition;
    }

    public void Respawn()
    {
        if (lastCheckpoint != Vector3.zero)
        {
            transform.position = lastCheckpoint;
            Debug.Log("Respawn in the checkpoint!");
        }
    }
}
