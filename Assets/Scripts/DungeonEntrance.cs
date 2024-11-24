using UnityEngine;
using UnityEngine.SceneManagement;

// Used to manage the entrance of the dungeons
public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] private KeyCode openEntranceKey;
    public GameObject entrance;
    public int necessarykeys;
    public Collider2D collider;
    bool canOpen;

    void Start()
    {
        entrance.SetActive(false);
    }

    void Update()
    {
        if (canOpen && Input.GetKeyDown(openEntranceKey))
        {
            OpenDungeon();
        }

    }

    public void OpenDungeon()
    {
        collider.enabled = false;
        entrance.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GlobalManager.Instance.keyCount == necessarykeys)
        {
            canOpen = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        canOpen = false;
    }
}
