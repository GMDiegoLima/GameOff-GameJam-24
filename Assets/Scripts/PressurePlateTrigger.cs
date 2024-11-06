using UnityEngine;
using UnityEngine.Events;

public class PressurePlateTrigger : MonoBehaviour
{
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;
    public GameObject player;
    PlayerController playerScript;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerScript.flying || other.CompareTag("PressureTrigger"))
        {
            animator.SetBool("Activated", true);
            onActivate?.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerScript.flying || other.CompareTag("PressureTrigger"))
        {
            animator.SetBool("Activated", false);
            onDeactivate?.Invoke();
        }
    }
}
