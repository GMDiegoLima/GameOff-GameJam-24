using UnityEngine;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour
{
    public List<Transform> waypoints = new List<Transform>();
    public float speed;
    int currentWaypointIndex = 0;
    bool hasReachedEnd = false;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (waypoints.Count == 0 || hasReachedEnd) return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];

        Vector2 direction = (targetWaypoint.position - transform.position).normalized;

        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);

        if (direction != Vector2.zero)
        {
            animator.SetFloat("LastHorizontal", direction.x);
            animator.SetFloat("LastVertical", direction.y);
        }

        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                hasReachedEnd = true;
            }
        }
    }
}
