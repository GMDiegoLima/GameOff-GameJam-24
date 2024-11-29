using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class PuzzleBalanceTwo : MonoBehaviour
{
    public PlayerController player;
    public GameObject leftPlate;
    public GameObject rightPlate;
    public TextMeshProUGUI addWeightText;
    public int maxItems;
    public List<Transform> weightsPositions;
    public List<GameObject> boxes;
    Vector3 leftPlateOrigin;
    Vector3 rightPlateOrigin;
    int holdingWeight;
    int checkWeight = 0;
    bool canAdd;
    GameObject item;
    Collider2D itemCollider;
    List<int> leftWeights = new List<int>();
    public int rightWeight;
    public UnityEvent onPuzzleSolved;
    public UnityEvent onPuzzleFail;

    void Start()
    {
        leftPlateOrigin = leftPlate.transform.position;
        rightPlateOrigin = rightPlate.transform.position;
    }

    void Update()
    {
        if (canAdd && Input.GetKeyDown("g"))
        {
            if (leftWeights.Count < maxItems)
            {
                leftWeights.Add(holdingWeight);
                player.DropItem();
                itemCollider.enabled = false;
                item.transform.SetParent(weightsPositions[leftWeights.Count - 1]);
                item.transform.position = weightsPositions[leftWeights.Count - 1].position;
                Vector3 leftPlatePosition = leftPlate.transform.position;
                Vector3 rightPlatePosition = rightPlate.transform.position;
                leftPlatePosition.y -= holdingWeight*0.01f;
                leftPlate.transform.position = leftPlatePosition;
                rightPlatePosition.y += holdingWeight*0.01f;
                rightPlate.transform.position = rightPlatePosition;
                holdingWeight = 0;
                checkWeight = 0;
                foreach (int weight in leftWeights)
                {
                    checkWeight += weight;
                }
                if (checkWeight == rightWeight)
                {
                    Debug.Log("Puzzle solved");
                    onPuzzleSolved.Invoke();
                }
                else
                {
                    Debug.Log("Wrong weight: " + checkWeight);
                    onPuzzleFail.Invoke();
                }
            }
            else
            {
                Debug.Log("Need to solve the puzzle using max of " + maxItems + " items");
            }
        }
    }

    public void ResetBalance()
    {
        leftWeights.Clear();
        checkWeight = 0;
        leftPlate.transform.position = leftPlateOrigin;
        rightPlate.transform.position = rightPlateOrigin;
        foreach (Transform weightPosition in weightsPositions)
        {
            foreach (Transform child in weightPosition)
            {
                Destroy(child.gameObject);
            }
        }
        foreach (GameObject box in boxes)
        {
            box.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            canAdd = true;
            addWeightText.enabled = true;
            item = other.gameObject;
            itemCollider = other.GetComponent<Collider2D>();
            holdingWeight = other.GetComponent<Item>().weight;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canAdd = false;
        addWeightText.enabled = false;
    }
}
