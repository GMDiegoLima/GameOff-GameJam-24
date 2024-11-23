using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PuzzleMemory : MonoBehaviour
{
    public SpriteRenderer[] platforms;
    public UnityEvent onPuzzleSolved;
    public UnityEvent onPuzzleReset;
    int[] sequence;
    int playerIndex = 0;

    void Start()
    {
        sequence = new int[platforms.Length];
        for (int i = 0; i < platforms.Length; i++)
        {
            sequence[i] = i;
            platforms[i].enabled = false;
        }
    }

    public void CheckPlatform(int platform)
    {
        if (platform == playerIndex)
        {
            platforms[sequence[playerIndex]].enabled = true;
            playerIndex++;
            if (playerIndex >= sequence.Length)
            {
                Debug.Log("Puzzle Solved !");
                onPuzzleSolved.Invoke();
            }
        }
    }
    public void ShowSequence()
    {
        StartCoroutine(DisplaySequence()); 
    }

    IEnumerator DisplaySequence()
    {
        foreach (int index in sequence)
        {
            platforms[index].enabled = true;
            yield return new WaitForSeconds(0.25f);
            platforms[index].enabled = false;
            yield return new WaitForSeconds(0.25f);
        }
    }
}