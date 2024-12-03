using UnityEngine;

public class PileOfItems : MonoBehaviour
{
    public GameObject item;
    public Transform pileOfItems;
    Collider2D spawnedItemCollider;
    bool canPick;

    void Update()
    {
        if (Input.GetKeyDown("e") && canPick && pileOfItems.childCount < 2)
        {
            if (item != null)
            {
                GameObject spawnedItem = Instantiate(item, transform.position, Quaternion.identity);
                item = spawnedItem;
                spawnedItemCollider = spawnedItem.GetComponent<Collider2D>();
                switch (item.name)
                {
                    case "bone":
                    {
                        spawnedItem.transform.localScale = new Vector3(1f, 1f, 1f);
                        break;
                    }
                    case "Apple":
                    {
                        spawnedItem.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        break;
                    }
                    case "emerald":
                    {
                        spawnedItem.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                        break;
                    }
                    default:
                        spawnedItem.transform.localScale = new Vector3(1f, 1f, 1f);
                        break;
                }
                spawnedItem.transform.SetParent(pileOfItems);
                spawnedItem.transform.localPosition = Vector3.zero;
                spawnedItemCollider.enabled = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPick = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canPick = false;
    }
}