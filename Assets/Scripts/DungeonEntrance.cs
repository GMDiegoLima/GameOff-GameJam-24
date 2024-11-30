using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Used to manage the entrance of the dungeons
public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] private KeyCode openEntranceKey;
    public GameObject entrance;
    public int necessarykeys;
    public GameObject door;
    Collider2D doorCollider;
    Vector3 closedPosition;
    Vector3 openPosition;
    bool canOpen;
    bool openDoor = false;
    bool closeDoor = false;

    public GameObject KeyUI;
    public TextMeshProUGUI KeyUIText;


    void Start()
    {
        entrance.SetActive(false);
        doorCollider = door.GetComponent<Collider2D>();
        closedPosition = door.transform.position;
        openPosition = door.transform.position + Vector3.left * 1.2f;
    }

    void Update()
    {
        if (canOpen && Input.GetKeyDown(openEntranceKey))
        {
            OpenDungeon();
        }
    }
    void FixedUpdate()
    {
        if (openDoor && !closeDoor)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, openPosition, 0.2f * Time.deltaTime);
            if (door.transform.position == openPosition)
            {
                openDoor = false;
                AkSoundEngine.PostEvent("door_opens_stop", gameObject);
            }
        }
        if (!openDoor && closeDoor)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, closedPosition, 2f * Time.deltaTime);
            if (door.transform.position == closedPosition)
            {
                closeDoor = false;
                AkSoundEngine.PostEvent("door_opens_stop", gameObject);
            }
        }
    }

    public void OpenDungeon()
    {
        openDoor = true;
        closeDoor = false;
        AkSoundEngine.PostEvent("door_opens", gameObject);
        doorCollider.enabled = false;
        entrance.SetActive(true);
    }
    public void CloseDungeon()
    {
        openDoor = false;
        closeDoor = true;
        AkSoundEngine.PostEvent("door_opens", gameObject);
        doorCollider.enabled = true;
        entrance.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        KeyUIText.text = GlobalManager.Instance.keyCount.ToString();
        KeyUI.SetActive(true);
        if (GlobalManager.Instance.keyCount == necessarykeys)
        {
            canOpen = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        canOpen = false;
        KeyUI.SetActive(false);
    }
}
