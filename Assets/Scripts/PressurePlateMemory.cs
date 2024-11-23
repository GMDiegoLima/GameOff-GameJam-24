using UnityEngine;
using System.Collections.Generic;

public class PressurePlateMemory : MonoBehaviour
{
    public GameObject player;
    PlayerController playerScript;
    public PuzzleMemory puzzleManager;
    public int plateInt;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerScript = player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player") && !playerScript.flying) || other.CompareTag("PressureTrigger"))
        {
            puzzleManager.CheckPlatform(plateInt);
            animator.SetBool("Activated", true);
            AkSoundEngine.PostEvent("plate", gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if ((other.CompareTag("Player") && !playerScript.flying) || other.CompareTag("PressureTrigger"))
        {
            animator.SetBool("Activated", false);
        }
    }
}
