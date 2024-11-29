using UnityEngine;
using UnityEngine.Events;

public class WireManager : MonoBehaviour
{
    public static WireManager Instance;
    public WireRotation[] cables;
    bool puzzleSolved = false;
    public UnityEvent onPuzzleSuccess;
    public UnityEvent onPuzzleFail;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        CheckPuzzleStatus();
    }

    public void CheckPuzzleStatus()
    {
        if (puzzleSolved) return;

        foreach (WireRotation cable in cables)
        {
            if (!cable.enabled)
            {
                onPuzzleFail?.Invoke();
                return;
            }
        }
        puzzleSolved = true;
        onPuzzleSuccess?.Invoke();
    }
}
