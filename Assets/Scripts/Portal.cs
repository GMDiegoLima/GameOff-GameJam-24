using UnityEngine;
using UnityEngine.SceneManagement;

// Used to change scenes when player enter
public class Portal : MonoBehaviour
{
    public scenes selectedScene;
    public enum scenes
    {
        Village,
        Dungeon,
        Graveyard
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadSceneAsync(selectedScene.ToString());
        }
    }
}