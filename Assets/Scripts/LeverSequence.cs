using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class LeverSequence : MonoBehaviour
{
    bool canPull;
    public bool activated;
    public TextMeshProUGUI pullText;
    public string leverName;
    public PuzzleSequenceManager puzzleSeqManager;
    public PuzzleActivations puzzleActManager;
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
            if (puzzleSeqManager != null)
            {
                PuzzleSequence activePuzzle = puzzleSeqManager.GetActivePuzzle();
                if (activated)
                {
                    activePuzzle.RegisterActivation(leverName);
                }
                else
                {
                    activePuzzle.RemoveActivation(leverName);
                }
            }
            if (puzzleActManager != null)
            {
                if (activated)
                {
                    if (puzzleActManager.IsNextInSequence(leverName))
                    {
                        Debug.Log("Correct lever");
                        AkSoundEngine.PostEvent("boss_correct_01", gameObject);
                    }
                    puzzleActManager.RegisterActivation(leverName);
                }
                else
                {
                    puzzleActManager.RemoveActivation(leverName);
                }
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