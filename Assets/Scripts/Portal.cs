using UnityEngine;
using UnityEngine.SceneManagement;

// Used to change scenes when player enter
public class Portal : MonoBehaviour
{
    public scenes selectedScene;
    public enum scenes
    {
        Village,
        Dungeon1,
        Dungeon2,
        Dungeon3,
        FinalBoss,
        Graveyard
    }
    public Vector2 playerSpawnPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadSceneAsync(selectedScene.ToString());
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerSpawnPosition;
        }
    }
}