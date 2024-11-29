using UnityEngine;

// Used to teach player turning on a tip
public class TutorialTIp : MonoBehaviour
{
    public GameObject attackTip;

    void Start()
    {
        attackTip.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            attackTip.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            attackTip.SetActive(false);
        }
    }
}
