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
    public TextMeshProUGUI buyText;
    public Transform hatPosition;
    public Transform glassesPosition;
    bool canShop;
    bool bought = false;

    void Start()
    {
        buyText.enabled = false;
        Item itemComponent = shopItem.GetComponent<Item>();
        if (itemComponent != null && GlobalManager.Instance.IsItemOwned(itemComponent.name, itemType))
        {
            bought = true;
            EquipItem();
        }
    }

    void Update()
    {
        if (canShop && Input.GetKeyDown(buyKey))
        {
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
                Debug.Log("Not enough gold to buy this item");
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !bought)
        {
            buyText.enabled = true;
            canShop = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        buyText.enabled = false;
        canShop = false;
    }
}
