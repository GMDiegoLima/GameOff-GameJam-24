using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GlobalManager.Instance.AddKey();
            Destroy(gameObject);
        }
    }
}
