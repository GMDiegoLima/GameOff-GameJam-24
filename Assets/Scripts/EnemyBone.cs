using UnityEngine;

public class EnemyBone : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 2f);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth;
            if (other.TryGetComponent<Health>(out playerHealth))
            {
                playerHealth.TakeDamage(1f);
                Destroy(gameObject);
            }
        }
    }
}
