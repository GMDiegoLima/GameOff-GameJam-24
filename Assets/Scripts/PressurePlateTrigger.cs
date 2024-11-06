using UnityEngine;
using UnityEngine.Events;

public class PressurePlateTrigger : MonoBehaviour
{
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PressureTrigger"))
        {
            animator.SetBool("Activated", true);
            onActivate?.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PressureTrigger"))
        {
            animator.SetBool("Activated", false);
            onDeactivate?.Invoke();
        }
    }
}
