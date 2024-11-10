using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleSequence : MonoBehaviour
{
    public List<string> correctSequence;

    public UnityEvent onPuzzleSolved;
    public UnityEvent onPuzzleReset;

    private List<string> playerSequence = new List<string>();
    [HideInInspector]
    public bool puzzleSolved;

    public void RegisterActivation(string triggerName)
    {
        if (puzzleSolved) return;

        playerSequence.Add(triggerName);
        Debug.Log($"Sequence updated: {string.Join(", ", playerSequence)}");
        if (!IsCorrectSoFar())
        {
            ResetPuzzle();
            return;
        }

        if (playerSequence.Count == correctSequence.Count && IsSequenceCorrect())
        {
            puzzleSolved = true;
            Debug.Log("Puzzle Solved!");
            onPuzzleSolved.Invoke();
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
        Debug.Log("Wrong sequence! Puzzle Reseted.");
        playerSequence.Clear();
        onPuzzleReset.Invoke();
    }

    public void ResetPuzzleState()
    {
        puzzleSolved = false;
        playerSequence.Clear();
    }

    public void RemoveItem(string triggerName)
    {
        playerSequence.Remove(triggerName);
    }
}
