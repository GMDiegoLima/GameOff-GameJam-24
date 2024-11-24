using UnityEngine;
using UnityEngine.Events;

// Call one event when someone enter the trigger
public class BossTrigger : MonoBehaviour
{
    public UnityEvent onActivate;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onActivate?.Invoke();
        }
    }
}
