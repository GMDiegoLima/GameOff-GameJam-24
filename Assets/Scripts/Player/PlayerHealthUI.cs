using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Health playerHealth;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        UpdateHeartsUI();
    }

    void UpdateHeartsUI()
    {
        if (playerHealth == null) return;

        float healthPerHeart = playerHealth.maxHealth / hearts.Length;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (playerHealth.currentHealth > i * healthPerHeart)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}
