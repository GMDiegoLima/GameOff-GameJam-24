using UnityEngine;

public class DoorNoWired : MonoBehaviour
{
    public WireConnection connectedWire;
    Vector3 openPosition;
    bool open;
    bool soundPlayed = false;

    void Start()
    {
        openPosition = transform.position + Vector3.down * 1.1f;
    }
    void Update()
    {
        if (connectedWire != null && !connectedWire.enabled)
        {
            if (!soundPlayed)
            {
                soundPlayed = true;
                AkSoundEngine.PostEvent("door_opens", gameObject);
            }
            if (transform.position == openPosition)
            {
                AkSoundEngine.PostEvent("door_opens_stop", gameObject);
                return;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, openPosition, 0.2f * Time.deltaTime);
            }
        }
    }
}
