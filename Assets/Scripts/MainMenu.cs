using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        AkSoundEngine.SetState("GameStatus", "InMenu");
    }

    public void PlayGame()
    {
        AkSoundEngine.SetState("GameStatus", "InGame");
        SceneManager.LoadSceneAsync(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
