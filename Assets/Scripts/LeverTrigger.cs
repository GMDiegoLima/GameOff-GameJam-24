using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LeverTrigger : MonoBehaviour
{
    bool canPull;
    public bool activated;
    public TextMeshProUGUI pullText;
    public UnityEvent onActivate;
    public UnityEvent onDeactivate;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    void Update()
    {
        if (canPull && Input.GetKeyDown("e"))
        {
            animator.enabled = true;
            activated = !activated;
            animator.SetBool("Activated", activated);
            AkSoundEngine.PostEvent("lever", gameObject);
            if (activated)
            {
                onActivate?.Invoke();
            }
            else
            {
                onDeactivate?.Invoke();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null && !other.GetComponent<PlayerController>().flying)
        {
            canPull = true;
            pullText.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPull = false;
            pullText.enabled = false;
        }
    }
}
