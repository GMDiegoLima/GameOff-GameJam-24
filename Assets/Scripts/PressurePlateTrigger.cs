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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Activated", true);
            onActivate?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Activated", false);
            onDeactivate?.Invoke();
        }
    }
}
