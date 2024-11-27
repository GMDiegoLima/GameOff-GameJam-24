using UnityEngine;
using System.Collections.Generic;

public class PressurePlateSequence : MonoBehaviour
{
    PlayerController playerScript;
    public PuzzleSequenceManager puzzleSeqManager;
    public PuzzleActivations puzzleActManager;
    public string plateName;
    public AK.Wwise.Event stingerEvent;
    Animator animator;
    HashSet<Collider2D> collidingObjects = new HashSet<Collider2D>();

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (collidingObjects.Count > 0)
        {
            if (!animator.GetBool("Activated"))
            {
                animator.SetBool("Activated", true);
                AkSoundEngine.PostEvent("plate", gameObject);
                HandleActivation();
            }
        }
        else
        {
            if (animator.GetBool("Activated"))
            {
                animator.SetBool("Activated", false);
                HandleDeactivation();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() != null || other.CompareTag("PressureTrigger"))
        {
            playerScript = other.GetComponent<PlayerController>();
            if (other.CompareTag("PressureTrigger") || (other.CompareTag("Player") && !playerScript.flying))
            {
                collidingObjects.Add(other);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (collidingObjects.Contains(other))
        {
            collidingObjects.Remove(other);
        }
    }

    void HandleActivation()
    {
        if (puzzleSeqManager != null)
        {
            PuzzleSequence activePuzzle = puzzleSeqManager.GetActivePuzzle();
            if (activePuzzle != null)
            {
                activePuzzle.RegisterActivation(plateName);
            }

            if (activePuzzle != null && activePuzzle.puzzleSolved)
            {
                puzzleSeqManager.OnPuzzleSolved();
            }
        }
        else if (puzzleActManager != null)
        {
            puzzleActManager.RegisterActivation(plateName);

            if (puzzleActManager.IsPuzzleSolved())
            {
                Debug.Log("PuzzleActivations resolvido.");
            }
        }
    }

    void HandleDeactivation()
    {
        if (puzzleActManager != null)
        {
            puzzleActManager.RemoveActivation(plateName);
        }
    }

    public void TriggerSfx()
    {
        stingerEvent.Post(gameObject);
    }
}
