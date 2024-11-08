using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequencePuzzle : MonoBehaviour
{
    public List<string> correctSequence;

    public UnityEvent onPuzzleSolved;
    public UnityEvent onPuzzleReset;

    private List<string> playerSequence = new List<string>();
    private bool puzzleSolved;

    public void RegisterActivation(string triggerName)
    {
        if (puzzleSolved) return;

        playerSequence.Add(triggerName);
        if (!IsCorrectSoFar())
        {
            ResetPuzzle();
            return;
        }

        if (playerSequence.Count == correctSequence.Count && IsSequenceCorrect())
        {
            puzzleSolved = true;
            onPuzzleSolved.Invoke();
            Debug.Log("Puzzle Solved!");
        }
    }

    private bool IsCorrectSoFar()
    {
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }
        return true;
    }

    private bool IsSequenceCorrect()
    {
        if (playerSequence.Count != correctSequence.Count) return false;

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }
        return true;
    }

    private void ResetPuzzle()
    {
        playerSequence.Clear();
        onPuzzleReset.Invoke();
        Debug.Log("Wrong sequence! Puzzle Reseted.");
    }
}
