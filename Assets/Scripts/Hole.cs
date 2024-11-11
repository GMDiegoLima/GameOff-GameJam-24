using UnityEngine;

public class Hole : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            Invoke("gameOver", 0.1f);
        }
    }

    void gameOver()
    {
        // playerAnimator.Play("Falling");
        playerScript.alive = false;
    }
}
