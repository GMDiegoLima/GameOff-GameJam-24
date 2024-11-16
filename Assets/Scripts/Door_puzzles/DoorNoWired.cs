using UnityEngine;

public class DoorNoWired : MonoBehaviour
{
    public WireConnection connectedWire;
    Collider2D collider;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
    }
    void Update()
    {
        if (connectedWire != null && !connectedWire.enabled)
        {
            animator.SetBool("Open", true);
            collider.enabled = false;
        }
        else
        {
            animator.SetBool("Open", false);
            collider.enabled = false;
        }
    }
}
