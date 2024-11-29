using UnityEngine;

public class Key : MonoBehaviour
{
    Vector3 targetPosition;
    bool moveItem = false;

    void Update()
    {
        if (moveItem)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                moveItem = false;
                GlobalManager.Instance.AddKey();
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!moveItem && collision.CompareTag("Player"))
        {
            targetPosition = collision.transform.position;
            moveItem = true;
        }
    }
}
