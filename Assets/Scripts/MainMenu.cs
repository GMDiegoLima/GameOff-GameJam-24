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
    public void Click()
    {
        AkSoundEngine.PostEvent("menu_cursor_select", gameObject);
    }
    public void Back()
    {
        AkSoundEngine.PostEvent("menu_cursor_back", gameObject);
    }
}
