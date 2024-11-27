using UnityEngine;

// Used with Ejector to be shot
public class Projetile : MonoBehaviour
{
    public float damage = 1f;
    int collisionCount = 0;
    void Start()
    {
        AkSoundEngine.PostEvent("projectile_arrow", gameObject);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth;
            if (other.TryGetComponent<Health>(out playerHealth))
            {
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);
			}
        }
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth;
            if (other.TryGetComponent<Health>(out enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
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
