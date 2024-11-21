using UnityEngine;

// Save the position (checkpoint) of the player when he enter the area
public class Checkpoint : MonoBehaviour
{
    public Collider2D triggerCollider;
    Animator animator;
    void Start()
    {
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
                triggerCollider.enabled = false;
                Debug.Log("Checkpoint saved!");
            }
        }
    }
}
