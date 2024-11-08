using UnityEngine;
using UnityEngine.Events;

public class CableManager : MonoBehaviour
{
    public static CableManager Instance;
    public CableRotation[] cables;
    public UnityEvent onPuzzleSuccess;

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
        foreach (CableRotation cable in cables)
        {
            if (!cable.isCorrect)
            {
                return;
            }
        }
        onPuzzleSuccess?.Invoke();
    }
}
