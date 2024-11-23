using UnityEngine;

// Used to kill one character when fall into the hole
public class Poison : MonoBehaviour
{
    public GameObject player;
    PlayerController playerScript;
    Animator playerAnimator;

    void Start()
    {
        if (player != null)
        {
            playerScript = player.GetComponent<PlayerController>();
            playerAnimator = player.GetComponent<Animator>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerScript.currentBody != PlayerController.Bodies.Skeleton)
        {
            Invoke("gameOver", 0.1f);
        }
    }

    void gameOver()
    {
        playerAnimator.SetBool("Dead", true);
        playerScript.alive = false;
    }
}
