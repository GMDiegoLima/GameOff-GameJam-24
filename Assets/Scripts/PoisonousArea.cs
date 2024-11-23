using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class PoisonousArea : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float poisonCD;

    Collider2D collider; 

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
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

    void OnTriggerExit2D(Collider2D collision)
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


