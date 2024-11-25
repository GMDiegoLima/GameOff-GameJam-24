using UnityEngine;

public class PlayerBone : MonoBehaviour
{
    public float damage = 1f;
    private Rigidbody2D body;
    public bool canCauseDamage = true;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (body.linearVelocity.magnitude <= 0.01f)
        {
            canCauseDamage = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Use OnCollisionEnter because do want to collide with the circleCollider for embody
        if (canCauseDamage && collision.gameObject.CompareTag("Enemy"))
        {
            Health enemyHealth;
            if (collision.gameObject.TryGetComponent<Health>(out enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
                Destroy(gameObject);
			}
        }
    }
}
