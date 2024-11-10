using UnityEngine;
using TMPro;

public class PuzzleBalanceReset : MonoBehaviour
{
    public PuzzleBalance balance;
    public TextMeshProUGUI resetText;
    bool canReset;

    void Update()
    {
        if (canReset && Input.GetKeyDown("e"))
        {
            balance.ResetBalance();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        canReset = true;
        resetText.enabled = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        canReset = false;
        resetText.enabled = false;
    }
}
