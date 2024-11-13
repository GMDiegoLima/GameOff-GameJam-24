using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public GameObject blackoutPanel;
    Animator blackoutAnimator;
    public void loadVillage()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void loadGraveyard()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void loadDungeon()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void FadeIn()
    {
        blackoutAnimator = blackoutPanel.GetComponent<Animator>();
        blackoutAnimator.Play("FadeIn");
    }
}
