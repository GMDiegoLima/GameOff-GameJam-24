using UnityEngine;

// Used to change animation when the enemy die (alive be false)
public class EnemyBody : MonoBehaviour
{
    public PlayerController.Bodies bodyType;
    Animator animator;
    public bool alive = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (alive)
        {
            animator.SetBool("Dead", false);
            gameObject.tag = "Enemy";
        }
        else
        {
            animator.SetBool("Dead", true);
            gameObject.tag = "DeadBody";
        }
    }
}