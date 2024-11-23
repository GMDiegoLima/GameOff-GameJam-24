using UnityEngine;

// Used with Ejector to be shot
public class Projetile : MonoBehaviour
{
    int collisionCount = 0;
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
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth;
            if (other.TryGetComponent<Health>(out enemyHealth))
            {
                enemyHealth.TakeDamage(1f);
                Destroy(gameObject);
			}
        }
        if (other.CompareTag("Wall"))
        {
            collisionCount++;
            if (collisionCount > 1)
            {
                Destroy(gameObject);
            }
        }
    }
}
