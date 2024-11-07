using UnityEngine;

public class CableRotation : MonoBehaviour
{
    public float correctRotation;
    public bool isCorrect = false;
    bool canRotate = false;

    void Update()
    {
        if (Input.GetKeyDown("e") && canRotate)
        {
            transform.Rotate(0, 0, 90f);
            if (Mathf.Approximately(transform.eulerAngles.z, correctRotation))
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }
            CableManager.Instance.CheckPuzzleStatus();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canRotate = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canRotate = false;
        }
    }
}
