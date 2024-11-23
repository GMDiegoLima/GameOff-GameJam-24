using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using TMPro;

public class PuzzleBalance : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI addWeightText;
    public GameObject weightIndicator;
    public TextMeshProUGUI weightIndicatorText;
    public GameObject plate;
    public int correctweight;
    public int maxItems;
    public List<Transform> weightsPositions;
    Vector3 plateOrigin;
    int holdingWeight;
    int checkWeight = 0;
    bool canAdd;
    GameObject item;
    Collider2D itemCollider;
    List<int> weights = new List<int>();
    public UnityEvent onPuzzleSolved;
    public UnityEvent onPuzzleFail;

    void Start()
    {
        plateOrigin = plate.transform.position;
        weightIndicator.SetActive(false);
    }

    void Update()
    {
        if (canAdd && Input.GetKeyDown("g"))
        {
            if (weights.Count < maxItems)
            {
                weightIndicator.SetActive(true);
                weights.Add(holdingWeight);
                player.DropItem();
                itemCollider.enabled = false;
                item.transform.SetParent(weightsPositions[weights.Count - 1]);
                item.transform.position = weightsPositions[weights.Count - 1].position;
                Vector3 platePosition = plate.transform.position;
                platePosition.y -= holdingWeight*0.01f;
                plate.transform.position = platePosition;
                holdingWeight = 0;
                checkWeight = 0;
                foreach (int weight in weights)
                {
                    checkWeight += weight;
                }
                weightIndicatorText.text = checkWeight.ToString();
                if (checkWeight == correctweight)
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
        weights.Clear();
        checkWeight = 0;
        plate.transform.position = plateOrigin;
        foreach (Transform weightPosition in weightsPositions)
        {
            foreach (Transform child in weightPosition)
            {
                Destroy(child.gameObject);
            }
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
