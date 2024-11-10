using UnityEngine;

public class WireRotation : MonoBehaviour
{
    public float correctRotation;
    public bool enabled = false;
    bool canRotate = false;
    Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (enabled)
        {
            animator.SetBool("Enabled", true);
        }
        else
        {
            animator.SetBool("Enabled", false);
        }
        if (Input.GetKeyDown("e") && canRotate)
        {
            transform.Rotate(0, 0, 90f);
            if (Mathf.Approximately(transform.eulerAngles.z, correctRotation))
            {
                enabled = true;
            }
            else
            {
                enabled = false;
            }
            WireManager.Instance.CheckPuzzleStatus();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canRotate = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canRotate = false;
        }
    }
}
