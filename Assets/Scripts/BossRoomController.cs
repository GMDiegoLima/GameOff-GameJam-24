using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossRoomController : MonoBehaviour
{
    public static BossRoomController Instance { get; private set; }

    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject bossHealthUI;
    [SerializeField] ChaseMark playerChaseMark;
    [SerializeField] ChaseMark bossChaseMark;
    [SerializeField] PlayerController playerController;
    [SerializeField] FinalBoss boss;
    [SerializeField] EnemyGenerator portal;
    [SerializeField] GameObject dialogue;
    [SerializeField] Collider2D entranceTrigger;
    [SerializeField] List<GameObject> lights;

    // Set true by other class
    [HideInInspector] public bool playerEnteredTrigger; // Set true in DoorH::Close()
    [HideInInspector] public bool portalOpenTrigger;
    [HideInInspector] public bool gameStartTrigger;
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
        bossHealthUI.SetActive(false);
        foreach (var l in lights)
        {
            l.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerEnteredTrigger)
        {
            StartCoroutine(PlayerEnterRoutine());
		}
        if (portalOpenTrigger)
        {
            StartCoroutine(OpenPortalRoutine());
		}
        if (gameStartTrigger)
        {
            StartCoroutine(GameStartRoutine());
		}
    }

    public void StartCinematic()
    {
        playerEnteredTrigger = true;
    }

    public void OpenPortal()
    {
        portalOpenTrigger = true;
        entranceTrigger.enabled = false;
    }

    // Add some coroutines for chaseMark, open portal
    private IEnumerator PlayerEnterRoutine()
    {
        // Damp to bossCam and start conversation
        Debug.Log("Trigger PlayerEnterRoutine");
        playerEnteredTrigger = false; // call this routine once
        playerController.StopMove();
        playerController.isControllable = false;
        foreach (var l in lights)
        {
            l.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        playerChaseMark.TurnOn();
        AkSoundEngine.PostEvent("enemy_alert", gameObject);
        yield return new WaitForSeconds(1f);
        playerCam.SetActive(false);
        yield return new WaitForSeconds(2f);
        bossChaseMark.TurnOn();
        AkSoundEngine.PostEvent("enemy_alert", gameObject);
        playerChaseMark.TurnOff();
        yield return new WaitForSeconds(2f);
        bossChaseMark.TurnOff();
        // Start conversation
        Debug.Log("Start converstaion");
        dialogue.SetActive(true);
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
        yield return new WaitForSeconds(1f);
        portal.GenerateRandomEnemy();
        yield return new WaitForSeconds(0.5f);
        portal.GenerateRandomEnemy();
        yield return new WaitForSeconds(0.2f);
        boss.ChangeOutfit();
        yield return new WaitForSeconds(3f);
        gameStartTrigger = true;
    }

    private IEnumerator GameStartRoutine()
    {
        // Damp back to playerCam
        Debug.Log("Trigger GameStartRoutine");
        gameStartTrigger = false;
        isGameStart = true;
        playerCam.SetActive(true);
        yield return new WaitForSeconds(2f);
        dialogue.SetActive(false);
        bossHealthUI.SetActive(true);
        playerController.isControllable = true;
    }
}
