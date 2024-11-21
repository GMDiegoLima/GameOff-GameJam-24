using UnityEngine;

public class DoorNoWired : MonoBehaviour
{
    public WireConnection connectedWire;
    Vector3 openPosition;
    bool open;

    void Start()
    {
        openPosition = transform.position + Vector3.down * 1.1f;
    }
    void Update()
    {
        if (connectedWire != null && !connectedWire.enabled)
        {
            if (transform.position == openPosition)
            {
                return;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, openPosition, 0.2f * Time.deltaTime);
            }
        }
    }
}
