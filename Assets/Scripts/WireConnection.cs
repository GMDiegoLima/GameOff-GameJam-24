using UnityEngine;

public class WireConnection : MonoBehaviour
{
    [SerializeField] private KeyCode cutKey;
    public WireConnection straightWire;
    public WireRotation curvedWire;
    public bool enabled = false;
    bool canCut;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isStraightWireEnabled = straightWire != null && straightWire.enabled;
        bool isCurvedWireEnabled = curvedWire != null && curvedWire.enabled;
        if ((isStraightWireEnabled || isCurvedWireEnabled) && animator.GetBool("Bitten"))
        {
            enabled = false;
            animator.SetBool("Enabled", false);
        }
        if (((isStraightWireEnabled || isCurvedWireEnabled) || (straightWire == null && curvedWire == null)) && !animator.GetBool("Bitten"))
        {
            enabled = true;
            animator.SetBool("Enabled", true);
        }
        else
        {
            enabled = false;
            animator.SetBool("Enabled", false);
        }

        if (Input.GetKeyDown(cutKey) && canCut)
        {
            animator.SetBool("Bitten", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>().currentBody == PlayerController.Bodies.Goblin)
        {
            canCut = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canCut = false;
        }
    }
}
