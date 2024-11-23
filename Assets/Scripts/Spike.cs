using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour
{
    public float delayBeforeStart = 2f;
    public float cycleTime = 2f;
    public float activeTime = 1f;

    Animator animator;
    Collider2D collider;

    void Start()
    {
        animator = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        StartCoroutine(StartAfterDelay());
    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        StartCoroutine(CycleAction());
    }

    IEnumerator CycleAction()
    {
        while (true)
        {
            yield return new WaitForSeconds(cycleTime);
            ActivateAction();

            yield return new WaitForSeconds(activeTime);
            DeactivateAction();
        }
    }

    void ActivateAction()
    {
        collider.enabled = true;
        animator.SetBool("Enabled", true);
    }

    void DeactivateAction()
    {
        collider.enabled = false;
        animator.SetBool("Enabled", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerController>() != null && !other.GetComponent<PlayerController>().flying)
        {
            Health playerHealth;
            if (other.TryGetComponent<Health>(out playerHealth))
            {
                playerHealth.TakeDamage(10f);
            }
        }
        if (other.CompareTag("Enemy"))
        {
            Health enemyHealth;
            if (other.TryGetComponent<Health>(out enemyHealth))
            {
                enemyHealth.TakeDamage(10f);
            }
        }

    }
}
