using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject itemToDrop;
    Animator animator;
    bool canOpen;
    bool opened;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (canOpen && Input.GetKeyDown("e") && !opened)
        {
            Debug.Log("Chest open");
            opened = true;
            animator.SetBool("Open", true);
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
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
