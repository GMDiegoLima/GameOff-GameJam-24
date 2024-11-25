using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Set by StateController")]
    [HideInInspector] public float maxHealth;
    public float currentHealth;
    public bool isDead { get => currentHealth <= 0;}

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float anAmount)
    {
        AkSoundEngine.PostEvent("hurt", gameObject);
        currentHealth = Mathf.Clamp(currentHealth - anAmount, 0, maxHealth);
	}

    public void TakeHeal(float anAmount)
    { 
        currentHealth = Mathf.Clamp(currentHealth + anAmount, 0, maxHealth);
	
	}
}
