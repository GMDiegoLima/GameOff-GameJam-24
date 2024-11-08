using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequencePuzzle : MonoBehaviour
{
    public List<string> correctSequence;

    public UnityEvent onPuzzleSolved;
    public UnityEvent onPuzzleReset;

    List<string> playerSequence = new List<string>();
    bool puzzleSolved;

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

    bool IsCorrectSoFar()
    {
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }
        return true;
    }

    bool IsSequenceCorrect()
    {
        if (playerSequence.Count != correctSequence.Count) return false;

        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (playerSequence[i] != correctSequence[i])
                return false;
        }
        return true;
    }

    void ResetPuzzle()
    {
        playerSequence.Clear();
        onPuzzleReset.Invoke();
        Debug.Log("Wrong sequence! Puzzle Reseted.");
    }

    public void RemoveItem(string triggerName)
    {
        playerSequence.Remove(triggerName);
    }
}
