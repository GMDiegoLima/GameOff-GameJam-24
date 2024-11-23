using UnityEngine;
using UnityEngine.SceneManagement;

// Used to manage the entrance of the dungeons
public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] private KeyCode openEntranceKey;
    public Portal entrance;
    public int necessarykeys;
    public Collider2D collider;
    Animator animator;
    bool canOpen;

    void Start()
    {
        animator = GetComponent<Animator>();
        entrance.enabled = false;
    }

    void Update()
    {
        if (canOpen && Input.GetKeyDown(openEntranceKey))
        {
            animator.SetBool("Open", true);
            collider.enabled = false;
            entrance.enabled = true;
        }

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
