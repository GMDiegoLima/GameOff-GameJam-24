using UnityEngine;

public class Ejector : MonoBehaviour
{
    Animator animator;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public enum Directions
    {
        Up,
        Left,
        Right,
        Down
    }
    public Directions shootDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }

    public void Shot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            animator.enabled = true;
            animator.Play("Shot");
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = Vector2.zero;
                switch (shootDirection)
                {
                    case Directions.Up:
                        direction = Vector2.up;
                        break;
                    case Directions.Left:
                        direction = Vector2.left;
                        break;
                    case Directions.Right:
                        direction = Vector2.right;
                        break;
                    case Directions.Down:
                        direction = Vector2.down;
                        break;
                }
                rb.linearVelocity = direction * projectileSpeed;
            }
        }
    }
}
