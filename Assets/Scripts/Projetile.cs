using UnityEngine;

public class Projetile : MonoBehaviour
{
    PlayerController playerbody;
    EnemyBody enemyBody;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerbody = other.GetComponent<PlayerController>();
            if (playerbody != null)
            {
                playerbody.alive = false;
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            EnemyBody enemyBody = other.GetComponent<EnemyBody>();
            if (enemyBody != null)
            {
                enemyBody.alive = false;
            }
        }
    }
}
