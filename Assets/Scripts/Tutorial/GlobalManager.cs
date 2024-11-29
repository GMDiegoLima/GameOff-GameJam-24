using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// Used to disable dialogue and collider border on the village when the player finish the tutorial
public class GlobalManager : MonoBehaviour
{
    public static GlobalManager Instance { get; private set; }

    public bool tutorialFinished = false;
    public GameObject dialogueTutorial;
    public GameObject borderTutorial;

    public int keyCount = 0;
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
        if (scene.name == "Village")
        {
            if (tutorialFinished)
            {
                dialogueTutorial = GameObject.Find("DialogueTutorial");
                borderTutorial = GameObject.Find("BorderTutorial");
                dialogueTutorial.SetActive(false);
                borderTutorial.SetActive(false);
            }
            else
            {
                dialogueTutorial = GameObject.Find("DialogueTutorial");
                borderTutorial = GameObject.Find("BorderTutorial");
                dialogueTutorial.SetActive(true);
                borderTutorial.SetActive(true);
            }
        }
    }
    public void AddKey()
    {
        keyCount++;
        Debug.Log($"Keys colected: {keyCount}");
    }
    public void AddGold()
    {
        goldCoins++;
        Debug.Log($"Gold colected: {goldCoins}");
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
