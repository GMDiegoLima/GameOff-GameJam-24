using UnityEngine;
using System.Collections;

public class BossRoomController : MonoBehaviour
{
    public static BossRoomController Instance { get; private set; }

    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject BossCam;
    [SerializeField] ChaseMark playerChaseMark;
    [SerializeField] ChaseMark bossChaseMark;
    [SerializeField] PlayerController playerController;
    [SerializeField] FinalBoss boss;
    [SerializeField] EnemyGenerator portal;

    // Set true by other class
    public bool playerEnteredTrigger; // Set true in DoorH::Close()
    public bool portalOpenTrigger;
    public bool gameStartTrigger;
    public bool isGameStart;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        portal.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            portalOpenTrigger = true;
		}
        if (Input.GetKeyDown(KeyCode.U))
        {
            gameStartTrigger = true; // start spawn enemies
		}    

        if (playerEnteredTrigger)
        {
            // Damp to bossCam
            // Trigger conversation and make the boss open the portal
            StartCoroutine(PlayerEnterRoutine());
		}
        if (portalOpenTrigger)
        {
            StartCoroutine(OpenPortalRoutine());
		}
        if (gameStartTrigger)
        {
            StartCoroutine(GameStartRoutine());
            //portal.gameObject.SetActive(true);
		}
    }

    // Add some coroutines for chaseMark, open portal
    private IEnumerator PlayerEnterRoutine()
    {
        // Damp to bossCam and have conversation
        Debug.Log("Trigger PlayerEnterRoutine");
        playerEnteredTrigger = false; // call this routine once
        playerController.StopMove();
        playerController.isControllable = false;
        playerChaseMark.TurnOn();
        yield return new WaitForSeconds(1f);
        playerCam.SetActive(false);
        yield return new WaitForSeconds(2f);
        bossChaseMark.TurnOn();
        playerChaseMark.TurnOff();
        yield return new WaitForSeconds(2f);
        bossChaseMark.TurnOff();
        // Start conversation
        Debug.Log("Start converstaion");
    }

    private IEnumerator OpenPortalRoutine()
    {
        // Boss open portal and change outfit
        Debug.Log("Trigger OpenPortalRoutine");
        portalOpenTrigger = false;
        portal.gameObject.SetActive(true);
        float portalScaleX = 0;
        float portalScaleY = portal.transform.localScale.y;
	    while (portalScaleX <= 1.5f)
        {
            portalScaleX += 0.2f;
            portal.transform.localScale = new Vector2(portalScaleX, portalScaleY);
            yield return new WaitForSeconds(0.2f);
        }
        boss.ChangeOutfit();
    }

    private IEnumerator GameStartRoutine()
    {
        // Damp back to playerCam
        Debug.Log("Trigger GameStartRoutine");
        gameStartTrigger = false;
        isGameStart = true;
        playerCam.SetActive(true);
        yield return new WaitForSeconds(2f);
        playerController.isControllable = true;
    }
}
