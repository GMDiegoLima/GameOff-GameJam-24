using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

// Used to disable dialogue and collider border on the village when the player finish the tutorial
public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    public bool tutorialFinished = false;
    public GameObject dialogueTutorial;
    public GameObject borderTutorial;

    GameObject keyUI;
    TextMeshProUGUI keyUIText;
    public int keyCount = 0;
    GameObject goldUI;
    TextMeshProUGUI goldUIText;
    public int goldCoins = 0;

    public List<string> ownedHats = new List<string>();
    public List<string> ownedGlasses = new List<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        keyUI = GameObject.Find("KeyUI");
        goldUI = GameObject.Find("GoldUI");
        if (keyUI != null && goldUI != null)
        {
            keyUIText = keyUI.GetComponentInChildren<TextMeshProUGUI>();
            goldUIText = goldUI.GetComponentInChildren<TextMeshProUGUI>();
        }
        if (scene.name == "Village")
        {
            if (tutorialFinished)
            {
                AkSoundEngine.SetState("MusicState", "Village");
                dialogueTutorial = GameObject.Find("DialogueTutorial");
                borderTutorial = GameObject.Find("BorderTutorial");
                dialogueTutorial.SetActive(false);
                borderTutorial.SetActive(false);
            }
            else
            {
                AkSoundEngine.SetState("MusicState", "Tutorial");
                dialogueTutorial = GameObject.Find("DialogueTutorial");
                borderTutorial = GameObject.Find("BorderTutorial");
                dialogueTutorial.SetActive(true);
                borderTutorial.SetActive(true);
            }
        }
        switch (scene.name)
        {
            case "Graveyard":
                AkSoundEngine.SetState("MusicState", "Tutorial");
                break;
            case "Dungeon1":
                AkSoundEngine.SetState("MusicState", "Dungeon1");
                break;
            case "Dungeon2":
                AkSoundEngine.SetState("MusicState", "Dungeon2");
                break;
            case "Dungeon3":
                AkSoundEngine.SetState("MusicState", "Dungeon3");
                break;
            case "FinalBoss":
                AkSoundEngine.SetState("MusicState", "Boss");
                break;
        }
    }
    public void AddKey()
    {
        keyCount++;
        StartCoroutine(BlinkUI(keyUI, keyUIText, "key"));
        Debug.Log($"Keys colected: {keyCount}");
    }
    public void AddGold()
    {
        goldCoins++;
        StartCoroutine(BlinkUI(goldUI, goldUIText, "gold"));
        Debug.Log($"Gold colected: {goldCoins}");
    }
    IEnumerator BlinkUI(GameObject obj, TextMeshProUGUI objText, string item)
    {
        if (item == "gold")
        {
            objText.text = goldCoins.ToString();
        }
        if (item == "key")
        {
            objText.text = keyCount.ToString();
        }
        obj.SetActive(true);
        yield return new WaitForSeconds(3f);
        obj.SetActive(false);
    }
    public void RemoveGold(int quantity)
    {
        goldCoins -= quantity;
        Debug.Log($"Gold colected: {goldCoins}");
    }
    public void AddOwnedItem(string itemName, ItemShop.ItemType itemType)
    {
        switch (itemType)
        {
            case ItemShop.ItemType.Hat:
                if (!ownedHats.Contains(itemName))
                    ownedHats.Add(itemName);
                break;

            case ItemShop.ItemType.Glasses:
                if (!ownedGlasses.Contains(itemName))
                    ownedGlasses.Add(itemName);
                break;
        }
    }
    public bool IsItemOwned(string itemName, ItemShop.ItemType itemType)
    {
        return itemType == ItemShop.ItemType.Hat 
            ? ownedHats.Contains(itemName) 
            : ownedGlasses.Contains(itemName);
    }
}
