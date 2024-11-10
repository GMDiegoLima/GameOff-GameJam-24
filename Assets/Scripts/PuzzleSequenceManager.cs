using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequenceManager : MonoBehaviour
{
    public List<PuzzleSequence> puzzles;
    private int currentPuzzleIndex = 0;

    void Start()
    {
        SetPuzzleActive(currentPuzzleIndex);
    }
    public void SetPuzzleActive(int puzzleIndex)
    {
        if (currentPuzzleIndex < puzzles.Count)
        {
            puzzles[currentPuzzleIndex].ResetPuzzleState();
            puzzles[currentPuzzleIndex].gameObject.SetActive(false);
        }

        if (puzzleIndex < puzzles.Count)
        {
            puzzles[puzzleIndex].gameObject.SetActive(true);
            currentPuzzleIndex = puzzleIndex;
        }
    }

    public PuzzleSequence GetActivePuzzle()
    {
        if (currentPuzzleIndex < puzzles.Count)
        {
            return puzzles[currentPuzzleIndex];
        }
        return null;
    }

    public void OnPuzzleSolved()
    {
       if (currentPuzzleIndex + 1 < puzzles.Count)
        {
            SetPuzzleActive(currentPuzzleIndex + 1);
        }
        else
        {
            Debug.Log("All puzzles have been solved");
        }
    }
}
