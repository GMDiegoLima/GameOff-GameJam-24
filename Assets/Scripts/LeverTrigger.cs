using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LeverTrigger : MonoBehaviour
{
    private bool canPull;
    public bool activated;
    public TextMeshProUGUI pullText;
    public UnityEvent onActivate;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canPull && Input.GetKeyDown("e"))
        {
            activated = !activated;
            if (activated)
            {
                animator.SetBool("Activated", true);
                onActivate?.Invoke();
            }
            else
            {
                animator.SetBool("Activated", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPull = true;
            pullText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPull = false;
            pullText.enabled = false;
        }
    }
}
