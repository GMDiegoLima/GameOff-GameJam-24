using UnityEngine;

public class TutorialFollow : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float minDistance;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        if (Vector2.Distance(transform.position, target.transform.position) < minDistance)
        {
            direction = Vector2.zero;
        }

        if (direction != Vector2.zero)
        {
            animator.SetFloat("LastHorizontal", direction.x);
            animator.SetFloat("LastVertical", direction.y);
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }
}