using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzleBalance : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI addWeightText;
    public int correctweight;
    public int maxItems;
    public List<Transform> weightsPositions;
    int holdingWeight;
    int checkWeight = 0;
    bool canAdd;
    GameObject item;
    List<int> weights = new List<int>();

    void Update()
    {
        if (canAdd && Input.GetKeyDown("g"))
        {
            if (weights.Count < maxItems)
            {
                weights.Add(holdingWeight);
                player.DropItem();
                item.transform.SetParent(weightsPositions[weights.Count - 1]);
                item.transform.position = weightsPositions[weights.Count - 1].position;
                holdingWeight = 0;
                checkWeight = 0;
                foreach (int weight in weights)
                {
                    checkWeight += weight;
                }
                if (checkWeight == correctweight)
                {
                    Debug.Log("Puzzle solved");
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
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            canAdd = true;
            addWeightText.enabled = true;
            item = other.gameObject;
            holdingWeight = other.GetComponent<Item>().weight;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canAdd = false;
        addWeightText.enabled = false;
    }
}
