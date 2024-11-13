using UnityEngine;

public class PileOfItems : MonoBehaviour
{
    public GameObject item;
    public Transform itemHoldPosition;
    bool canPick;

    void Update()
    {
        if (Input.GetKeyDown("e") && canPick && itemHoldPosition.childCount == 0)
        {
            if (item != null)
            {
                item.transform.SetParent(itemHoldPosition);
                item.transform.position = itemHoldPosition.position;
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