using UnityEngine;

public class FireProjetile : MonoBehaviour
{
    public float damage = 1f;
    int collisionCount = 0;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Cast");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth;
            if (other.TryGetComponent<Health>(out playerHealth))
            {
                playerHealth.TakeDamage(damage);
                animator.Play("Hit");
                Invoke("OnHit", 0.3f);
			}
        }
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth;
            if (other.TryGetComponent<Health>(out enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
                animator.Play("Hit");
                Invoke("OnHit", 0.3f);
			}
        }
        if (other.CompareTag("Wall"))
        {
            collisionCount++;
            if (collisionCount > 1)
            {
                animator.Play("Hit");
                Invoke("OnHit", 0.3f);
            }
        }
    }

    void OnHit()
    {
        Destroy(gameObject);
    }
}
