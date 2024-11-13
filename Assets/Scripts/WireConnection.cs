using UnityEngine;

public class WireConnection : MonoBehaviour
{
    public WireConnection straightWire;
    public WireRotation curvedWire;
    public bool enabled = false;
    bool canBite;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isStraightWireEnabled = straightWire != null && straightWire.enabled;
        bool isCurvedWireEnabled = curvedWire != null && curvedWire.enabled;
        if (isStraightWireEnabled || isCurvedWireEnabled)
        {
            enabled = true;
            animator.SetBool("Enabled", true);
        }
        else
        {
            enabled = false;
            animator.SetBool("Enabled", false);
        }

        if (Input.GetKeyDown("j") && canBite)
        {
            animator.SetBool("Bitten", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>().currentBody == PlayerController.Bodies.Wolf)
        {
            canBite = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.GetComponent<PlayerController>().currentBody != PlayerController.Bodies.Wolf)
        {
            canBite = false;
        }
    }
}
