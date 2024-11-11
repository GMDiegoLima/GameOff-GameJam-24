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
            animator.SetBool("Activated", true);
            puzzleManager.CheckPlatform(plateInt);
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
