using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class PoisonousArea : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float poisonCD;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<StateController>(out StateController aController))
        { 
            if (aController.actor.actorType != ActorType.Skeleton && 
                aController.actor.actorType != ActorType.Ghost &&
				collision.TryGetComponent<Health>(out Health aHealth))
            {
                aController.isPoisoned = true;
                StartCoroutine(PoisonRoutine(aController, aHealth));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    { 
        if (collision.TryGetComponent<StateController>(out StateController aController))
        {
            aController.isPoisoned = false;
        }
    }


    IEnumerator PoisonRoutine(StateController aController, Health aHealth)
    {
        while (aController.isPoisoned)
        {
            aHealth.TakeDamage(0.5f);
            Debug.Log("PoisonousArea take damage by 0.5f");
            yield return new WaitForSeconds(poisonCD);
        }
    }
}


