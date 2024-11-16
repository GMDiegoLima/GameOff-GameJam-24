using UnityEngine;
using UnityEngine.Events;

// Call one event when someone enter the trigger of the Pressure Plate
public class PressurePlateTrigger : MonoBehaviour
{
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;
    PlayerController playerScript;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null || other.CompareTag("PressureTrigger") || other.CompareTag("Item"))
        {
            playerScript = other.GetComponent<PlayerController>();
            if ((other.CompareTag("Player") && !playerScript.flying) || other.CompareTag("PressureTrigger") || other.CompareTag("Item"))
            {
                animator.SetBool("Activated", true);
                AkSoundEngine.PostEvent("plate", gameObject);
                onActivate?.Invoke();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null || other.CompareTag("PressureTrigger") || other.CompareTag("Item"))
        {
            playerScript = other.GetComponent<PlayerController>();
            if ((other.CompareTag("Player") && !playerScript.flying) || other.CompareTag("PressureTrigger") || other.CompareTag("Item"))
            {
                animator.SetBool("Activated", false);
                onDeactivate?.Invoke();
            }
        }
    }
}

