using UnityEngine;

// Used with Ejector to be shot
public class Projetile : MonoBehaviour
{
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
    }
}
