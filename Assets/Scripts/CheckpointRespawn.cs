using UnityEngine;
using UnityEngine.SceneManagement;

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
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("currentSceneName");
        if (currentSceneName == "FinalBoss")
        {
            SceneManager.LoadSceneAsync(6);
        }
        else
        {
            player.alive = true;
            player.fell = false;
            playerHealth.currentHealth = 3;
            checkpoint.Respawn();
        }
    }
}
