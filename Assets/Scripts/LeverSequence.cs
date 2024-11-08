using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LeverSequence : MonoBehaviour
{
    bool canPull;
    public bool activated;
    public TextMeshProUGUI pullText;
    public UnityEvent onActivate;
    public string leverName;
    public SequencePuzzle sequencePuzzle;
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
                onActivate?.Invoke();
                sequencePuzzle.RegisterActivation(leverName);
            }
            else
            {
                animator.SetBool("Activated", false);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
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