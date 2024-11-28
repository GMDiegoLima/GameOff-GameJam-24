using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Used when need to do a action after activate few things (levers / pressure plates)
public class PuzzleActivations : MonoBehaviour
{
    public List<string> requiredActivations;
    public UnityEvent onPuzzleSolved;
    public UnityEvent onPuzzleFail;

    HashSet<string> activatedLevers = new HashSet<string>();
    bool puzzleSolved;

    public void RegisterActivation(string triggerName)
    {
        activatedLevers.Add(triggerName);
        Debug.Log($"Activated: {string.Join(", ", activatedLevers)}");

        if (IsPuzzleSolved())
        {
            puzzleSolved = true;
            Debug.Log("Puzzle Solved!");
            AkSoundEngine.PostEvent("puzzle_success", gameObject);
            onPuzzleSolved.Invoke();
        }
        else
        {
            onPuzzleFail.Invoke();
        }
    }

    public void RemoveActivation(string triggerName)
    {
        activatedLevers.Remove(triggerName);
        if (puzzleSolved && !IsPuzzleSolved())
        {
            puzzleSolved = false;
            Debug.Log("Puzzle Failed!");
            onPuzzleFail.Invoke();
        }
        if (IsPuzzleSolved())
        {
            puzzleSolved = true;
            Debug.Log("Puzzle Solved!");
            AkSoundEngine.PostEvent("puzzle_success", gameObject);
            onPuzzleSolved.Invoke();
        }
        else
        {
            onPuzzleFail.Invoke();
        }
    }

    public bool IsPuzzleSolved()
    {
        return activatedLevers.SetEquals(requiredActivations);
    }
    public bool IsNextInSequence(string leverName)
    {
        return requiredActivations.Contains(leverName);
    }
}
