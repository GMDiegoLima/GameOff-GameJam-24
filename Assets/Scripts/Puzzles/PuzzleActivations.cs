using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Used when need to do a action after activate few things (levers / pressure plates)
public class PuzzleActivations : MonoBehaviour
{
    public List<string> requiredActivations;
    public UnityEvent onPuzzleSolved;

    private HashSet<string> activatedLevers = new HashSet<string>();
    bool puzzleSolved;

    public void RegisterActivation(string triggerName)
    {
        if (puzzleSolved) return;

        activatedLevers.Add(triggerName);
        Debug.Log($"Activated: {string.Join(", ", activatedLevers)}");

        if (IsPuzzleSolved())
        {
            puzzleSolved = true;
            Debug.Log("Puzzle Solved!");
            onPuzzleSolved.Invoke();
        }
    }

    public void RemoveActivation(string triggerName)
    {
        activatedLevers.Remove(triggerName);
    }

    public bool IsPuzzleSolved()
    {
        return activatedLevers.SetEquals(requiredActivations);
    }
}
