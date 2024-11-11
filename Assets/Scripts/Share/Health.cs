using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Set by StateController")]
    public float maxHealth;
    public float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float anAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth - anAmount, 0, maxHealth);
	}

    public void TakeHeal(float anAmount)
    { 
        currentHealth = Mathf.Clamp(currentHealth + anAmount, 0, maxHealth);
	
	}
}
