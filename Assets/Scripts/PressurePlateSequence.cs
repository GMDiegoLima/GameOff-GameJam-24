using UnityEngine;

public class PressurePlateSequence : MonoBehaviour
{
    public GameObject player;
    PlayerController playerScript;
    public SequencePuzzle sequencePuzzle;
    public string plateName;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerScript.flying || other.CompareTag("PressureTrigger"))
        {
            animator.SetBool("Activated", true);
            sequencePuzzle.RegisterActivation(plateName);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerScript.flying || other.CompareTag("PressureTrigger"))
        {
            animator.SetBool("Activated", false);
        }
    }
}
