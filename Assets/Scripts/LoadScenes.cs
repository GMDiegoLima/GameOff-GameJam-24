using UnityEngine;
using UnityEngine.SceneManagement;

// used on events to load scenes
public class LoadScenes : MonoBehaviour
{
    public GameObject blackoutPanel;
    Animator blackoutAnimator;
    void Start()
    {
        if (blackoutPanel != null)
        {
            blackoutAnimator = blackoutPanel.GetComponent<Animator>();
        }
    }
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
        blackoutAnimator.Play("FadeIn");
    }
    public void FadeOut()
    {
        blackoutAnimator.Play("FadeOut");
    }
}
