using UnityEngine;
using UnityEngine.Rendering.Universal;

// Save the position (checkpoint) of the player when he enter the area
public class Checkpoint : MonoBehaviour
{
    public Collider2D triggerCollider;
    public Transform spawnPosition;
    Light2D light;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        light = GetComponent<Light2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerCheckpoint player = collision.GetComponent<PlayerCheckpoint>();
            if (player != null)
            {
                AkSoundEngine.PostEvent("checkpoint", gameObject);
                animator.SetBool("Enabled", true);
                light.enabled = true;
                spawnPosition.position = new Vector3(spawnPosition.position.x, spawnPosition.position.y, 0);
                player.SetCheckpoint(spawnPosition.position);
                triggerCollider.enabled = false;
                Debug.Log("Checkpoint saved!");
            }
        }
    }
}
