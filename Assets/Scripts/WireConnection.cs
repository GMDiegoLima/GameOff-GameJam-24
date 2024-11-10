using UnityEngine;

public class WireConnection : MonoBehaviour
{
    public WireConnection straightWire;
    public WireRotation curvedWire;
    public bool enabled = false;
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
    }
}
