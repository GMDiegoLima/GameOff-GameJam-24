using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public GameObject credits;
    public GameObject blackoutPanel;
    Animation creditsAnimation;
    Animator blackoutAnimator;
    void Awake()
    {
        credits.SetActive(false);
        blackoutAnimator = blackoutPanel.GetComponent<Animator>();
        creditsAnimation = credits.GetComponent<Animation>();
    }
    public void StartCredits()
    {
        credits.SetActive(true);
        blackoutAnimator.Play("FadeIn");
        StartCoroutine(AnimateCredits());
    }
    IEnumerator AnimateCredits()
    {
        yield return new WaitForSeconds(1f);
        creditsAnimation.Play("credits");
        yield return new WaitForSeconds(20f);
        SceneManager.LoadSceneAsync(0);
    }
}
