using UnityEngine;

public class PileOfItems : MonoBehaviour
{
    public GameObject item;
    public Transform pileOfItems;
    bool canPick;

    void Update()
    {
        if (Input.GetKeyDown("e") && canPick && pileOfItems.childCount < 2)
        {
            if (item != null)
            {
                GameObject spawnedItem = Instantiate(item, transform.position, Quaternion.identity);
                spawnedItem.transform.localScale = new Vector3(1f, 1f, 1f);
                spawnedItem.transform.SetParent(pileOfItems);
                spawnedItem.transform.localPosition = Vector3.zero;
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