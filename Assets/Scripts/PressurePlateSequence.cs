using UnityEngine;
using System.Collections.Generic;

public class PressurePlateSequence : MonoBehaviour
{
    public GameObject player;
    PlayerController playerScript;
    public List<PuzzleSequence> puzzles;
    public PuzzleSequenceManager puzzleManager;
    public string plateName;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player") && !playerScript.flying) || other.CompareTag("PressureTrigger"))
        {
            animator.SetBool("Activated", true);

            PuzzleSequence activePuzzle = puzzleManager.GetActivePuzzle();
            if (activePuzzle != null)
            {
                activePuzzle.RegisterActivation(plateName);
            }
            else
            {
                Debug.LogWarning("Nenhum puzzle ativo encontrado.");
            }
        }

        PuzzleSequence currentPuzzle = puzzleManager.GetActivePuzzle();
        if (currentPuzzle != null && currentPuzzle.puzzleSolved)
        {
            puzzleManager.OnPuzzleSolved();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Player") && !playerScript.flying) || other.CompareTag("PressureTrigger"))
        {
            animator.SetBool("Activated", false);
        }
    }
}
