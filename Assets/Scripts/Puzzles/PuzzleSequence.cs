using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Create one sequence that need to be match to be solved
[System.Serializable]
public class SoundEvent
{
    public AK.Wwise.Event soundEvent;
}

public class PuzzleSequence : MonoBehaviour
{
    public List<string> correctSequence;
    public List<GameObject> platforms;

    public UnityEvent onPuzzleSolved;
    public UnityEvent onPuzzleReset;

    List<string> playerSequence = new List<string>();
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
            AkSoundEngine.PostEvent("path_wrong", gameObject);
            return;
        }

        if (playerSequence.Count == correctSequence.Count && IsSequenceCorrect())
        {
            puzzleSolved = true;
            Debug.Log("Puzzle Solved!");
            AkSoundEngine.PostEvent("puzzle_success", gameObject);
            onPuzzleSolved.Invoke();
        }
    }

    public void RemoveActivation(string triggerName)
    {
        playerSequence.Remove(triggerName);
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

    public void PlaySfxsSequence()
    {
        StartCoroutine(DisplaySequence()); 
    }

    IEnumerator DisplaySequence()
    {
        for (int i = 0; i < correctSequence.Count; i++)
        {
            if (i < platforms.Count)
            {
                PressurePlateSequence plateSequence = platforms[i].GetComponent<PressurePlateSequence>();
                platforms[i].SetActive(true);
                yield return new WaitForSeconds(0.25f);
                platforms[i].SetActive(false);
                yield return new WaitForSeconds(0.25f);
                platforms[i].SetActive(true);
                plateSequence.stingerEvent.Post(gameObject);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
