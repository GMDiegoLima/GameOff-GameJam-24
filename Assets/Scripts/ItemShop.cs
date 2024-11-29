using UnityEngine;
using TMPro;

public class ItemShop : MonoBehaviour
{
    [SerializeField] private KeyCode buyKey;
    public GameObject shopItem;
    public int itemPrice;
    public enum ItemType
    {
        Hat,
        Glasses,
    }
    public ItemType itemType;
    public GameObject buyTextPanel;
    public TextMeshProUGUI buyText;
    public Transform hatPosition;
    public Transform glassesPosition;
    public GameObject goldUI;
    public TextMeshProUGUI goldUIText;
    bool canShop;
    bool bought = false;

    void Start()
    {
        buyText.enabled = false;
        buyTextPanel.SetActive(false);
        Item itemComponent = shopItem.GetComponent<Item>();
        if (itemComponent != null && GlobalManager.Instance.IsItemOwned(itemComponent.name, itemType))
        {
            bought = true;
            EquipItem();
        }
    }

    void Update()
    {
        if (canShop)
        {
            goldUIText.text = GlobalManager.Instance.goldCoins.ToString();
            goldUI.SetActive(true);
            if (Input.GetKeyDown(buyKey))
            {
                if (IsItemAlreadyEquipped())
                {
                    buyText.text = "You already have one item of this type";
                    return;
                }
                if (GlobalManager.Instance.goldCoins >= itemPrice)
                {
                    GlobalManager.Instance.RemoveGold(itemPrice);
                    Item itemComponent = shopItem.GetComponent<Item>();
                    if (itemComponent != null)
                    {
                        GlobalManager.Instance.AddOwnedItem(itemComponent.name, itemType);
                    }
                    EquipItem();
                    bought = true;
                    Debug.Log($"bought one item per {itemPrice} of gold");
                }
                else
                {
                    buyText.text = "Not enough gold to buy this item";
                }
            }
        }
    }
    void EquipItem()
    {
        switch (itemType)
        {
            case ItemType.Hat:
                shopItem.transform.SetParent(hatPosition);
                shopItem.transform.position = hatPosition.position;
                break;

            case ItemType.Glasses:
                shopItem.transform.SetParent(glassesPosition);
                shopItem.transform.position = glassesPosition.position;
                break;
        }
    }
    bool IsItemAlreadyEquipped()
    {
        switch (itemType)
        {
            case ItemType.Hat:
                return GlobalManager.Instance.ownedHats.Count > 0;

            case ItemType.Glasses:
                return GlobalManager.Instance.ownedGlasses.Count > 0;

            default:
                return false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !bought)
        {
            buyText.enabled = true;
            buyText.text = $"Press E to buy this item per {itemPrice} of gold";
            buyTextPanel.SetActive(true);
            canShop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        buyText.enabled = false;
        buyTextPanel.SetActive(false);
        goldUI.SetActive(false);
        canShop = false;
    }
}
