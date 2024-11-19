using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject itemToDrop;
    bool canOpen;
    bool opened;

    void Update()
    {
        if (canOpen && Input.GetKeyDown("e") && !opened)
        {
            Debug.Log("Chest open");
            opened = true;
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !opened)
        {
            canOpen = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
}
