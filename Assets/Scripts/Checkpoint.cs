using UnityEngine;

// Save the position (checkpoint) of the player when he enter the area
public class Checkpoint : MonoBehaviour
{
    Collider2D collider;
    Animator animator;
    SpriteRenderer sprite;
    void Start()
    {
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerCheckpoint player = collision.GetComponent<PlayerCheckpoint>();
            if (player != null)
            {
                animator.SetBool("Enabled", true);
                player.SetCheckpoint(transform.position);
                collider.enabled = false;
                sprite.enabled = false;
                Debug.Log("Checkpoint saved!");
            }
        }
    }
}
