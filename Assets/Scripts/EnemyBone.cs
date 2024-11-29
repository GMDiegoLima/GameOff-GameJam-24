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
            // Ghost won't be hit
            if (other.GetComponent<PlayerStateController>().actor.actorType != ActorType.Ghost)
            {
                Health playerHealth;
                if (other.TryGetComponent<Health>(out playerHealth))
                {
                    playerHealth.TakeDamage(1f);
                    Destroy(gameObject);
                }
            }
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
		}
    }
}
