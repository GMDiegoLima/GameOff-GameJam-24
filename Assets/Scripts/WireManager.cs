using UnityEngine;
using UnityEngine.Events;

public class WireManager : MonoBehaviour
{
    public static WireManager Instance;
    public WireRotation[] cables;
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

    public void CheckPuzzleStatus()
    {
        foreach (WireRotation cable in cables)
        {
            if (!cable.enabled)
            {
                onPuzzleFail?.Invoke();
                return;
            }
        }
        onPuzzleSuccess?.Invoke();
    }
}
