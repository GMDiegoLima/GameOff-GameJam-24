using UnityEngine;

public class WireRotation : MonoBehaviour
{
    public WireConnection straightWire;
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
        UpdateCableState();
        animator.SetBool("Enabled", enabled);
        if (Input.GetKeyDown("e") && canRotate)
        {
            transform.Rotate(0, 0, 90f);
            AkSoundEngine.PostEvent("wire_turn", gameObject);
            UpdateCableState();
            WireManager.Instance.CheckPuzzleStatus();
        }
    }
    float NormalizeAngle(float angle)
    {
        while (angle < 0) angle += 360;
        while (angle >= 360) angle -= 360;
        return angle;
    }

    void UpdateCableState()
    {
        float currentAngle = NormalizeAngle(transform.eulerAngles.z);
        float targetAngle = NormalizeAngle(correctRotation);
        if (Mathf.Approximately(currentAngle, targetAngle) &&
            (straightWire != null && straightWire.enabled || straightWire == null))
        {
            enabled = true;
        }
        else
        {
            enabled = false;
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
