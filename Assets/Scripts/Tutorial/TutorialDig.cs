using UnityEngine;
using TMPro;

// All the actions necessary on the graveyard for the tutorial are here
public class TutorialDig : MonoBehaviour
{
    public PlayerController player;
    public GameObject background;
    public TextMeshProUGUI disembodyText;
    public TextMeshProUGUI digText;
    public GameObject flower;
    public Health skeletonHealth;
    SpriteRenderer sprite;
    bool canDig;
    bool dug = false;
    bool embodied = false;
    public GameObject lastDialogue;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown("e") && !player.flying && !dug && canDig)
        {
            dug = true;
            AkSoundEngine.PostEvent("coffin", gameObject);
            sprite.enabled = true;
            Destroy(flower);
            disembodyText.enabled = true;
            digText.enabled = false;
            transform.Find("Coffin").gameObject.SetActive(true);
            transform.Find("Skeleton").gameObject.SetActive(true);
            skeletonHealth.maxHealth = 0f;
        }

        if (Input.GetKeyDown("q") && dug && canDig)
        {
            embodied = true;
            disembodyText.enabled = false;
            background.SetActive(false);
        }
        if (player.currentBody == PlayerController.Bodies.Skeleton)
        {
            disembodyText.enabled = false;
            background.SetActive(false);
            digText.enabled = false;
            lastDialogue.SetActive(true);
        }
    }

    public void FinishTutorial()
    {
        GlobalManager.Instance.tutorialFinished = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        canDig = true;
        if (other.CompareTag("Player") && !player.flying && !dug && !embodied)
        {
            digText.enabled = true;
            background.SetActive(true);
            disembodyText.enabled = false;
        }
        if (other.CompareTag("Player") && !player.flying && dug && !embodied)
        {
            disembodyText.enabled = true;
            background.SetActive(true);
            digText.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canDig = false;
        background.SetActive(false);
        digText.enabled = false;
        disembodyText.enabled = false;
    }
}