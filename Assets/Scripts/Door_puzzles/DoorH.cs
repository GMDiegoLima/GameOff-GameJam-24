using UnityEngine;

public class DoorH : MonoBehaviour
{
    Vector3 openPosition;
    Vector3 closePosition;
    bool open = false;
    bool close = false;

    void Start()
    {
        closePosition = transform.position;
        openPosition = transform.position + Vector3.left * 1.1f;
    }

    void Update()
    {
        if (open && !close)
        {
            AkSoundEngine.PostEvent("door_opens", gameObject);
            transform.position = Vector3.MoveTowards(transform.position, openPosition, 0.2f * Time.deltaTime);
            if (transform.position == openPosition)
            {
                open = false;
            }
        }
        if (!open && close)
        {
            AkSoundEngine.PostEvent("door_opens", gameObject);
            transform.position = Vector3.MoveTowards(transform.position, closePosition, 0.4f * Time.deltaTime);
            if (transform.position == closePosition)
            {
                close = false;
            }
        }
    }

    public void OpenDoor()
    {
        open = true;
        close = false;
    }
    public void CloseDoor()
    {
        open = false;
        close = true;
    }
}
