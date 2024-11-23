using UnityEngine;

// Used to kill one character when fall into the abyss
public class Abyss : MonoBehaviour
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
        if (other.CompareTag("Player") && !playerScript.flying)
        {
            Invoke("gameOver", 0.4f);
        }
    }

    void gameOver()
    {
        playerScript.fell = true;
    }
}
