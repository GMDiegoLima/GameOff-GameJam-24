using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LeverTrigger : MonoBehaviour
{
    bool canPull;
    public bool activated;
    public TextMeshProUGUI pullText;
    public UnityEvent onActivate;
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
            if (activated)
            {
                animator.SetBool("Activated", true);
                AkSoundEngine.PostEvent("lever", gameObject);
                onActivate?.Invoke();
            }
            else
            {
                animator.SetBool("Activated", false);
                AkSoundEngine.PostEvent("lever", gameObject);
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
