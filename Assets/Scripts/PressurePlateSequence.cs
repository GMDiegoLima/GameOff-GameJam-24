using UnityEngine;
using System.Collections.Generic;

// Used for 2 puzzles (Sequence and Activations)
public class PressurePlateSequence : MonoBehaviour
{
    PlayerController playerScript;
    public PuzzleSequenceManager puzzleSeqManager;
    public PuzzleActivations puzzleActManager;
    public string plateName;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null || other.CompareTag("PressureTrigger"))
        {
            playerScript = other.GetComponent<PlayerController>();
            if ((other.CompareTag("Player") && !playerScript.flying) || other.CompareTag("PressureTrigger"))
            {
                animator.SetBool("Activated", true);
                AkSoundEngine.PostEvent("plate", gameObject);

                if (puzzleSeqManager != null)
                {
                    PuzzleSequence activePuzzle = puzzleSeqManager.GetActivePuzzle();
                    if (activePuzzle != null)
                    {
                        activePuzzle.RegisterActivation(plateName);
                    }
                }
                else if (puzzleActManager != null)
                {
                    puzzleActManager.RegisterActivation(plateName);
                }
            }

            if (puzzleSeqManager != null)
            {
                PuzzleSequence currentPuzzle = puzzleSeqManager.GetActivePuzzle();
                if (currentPuzzle != null && currentPuzzle.puzzleSolved)
                {
                    puzzleSeqManager.OnPuzzleSolved();
                }
            }

            else if (puzzleActManager != null && puzzleActManager.IsPuzzleSolved())
            {
                Debug.Log("PuzzleActivations resolvido.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null || other.CompareTag("PressureTrigger"))
        {
            playerScript = other.GetComponent<PlayerController>();
            if ((other.CompareTag("Player") && !playerScript.flying) || other.CompareTag("PressureTrigger"))
            {
                if (puzzleActManager != null)
                {
                    
                    puzzleActManager.RemoveActivation(plateName);
                }
                animator.SetBool("Activated", false);
            }
        }
    }
}
