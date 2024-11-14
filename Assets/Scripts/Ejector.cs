using UnityEngine;

// Used to shot one projectile from the walls
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
                float rotationAngle = 0f;
                switch (shootDirection)
                {
                    case Directions.Up:
                        direction = Vector2.up;
                        rotationAngle = 180f;
                        break;
                    case Directions.Left:
                        direction = Vector2.left;
                        rotationAngle = -90f;
                        break;
                    case Directions.Right:
                        direction = Vector2.right;
                        rotationAngle = 90f;
                        break;
                    case Directions.Down:
                        direction = Vector2.down;
                        rotationAngle = 0f;
                        break;
                }
                projectile.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
                rb.linearVelocity = direction * projectileSpeed;
                Destroy(projectile, 5f);
            }
        }
    }
}
