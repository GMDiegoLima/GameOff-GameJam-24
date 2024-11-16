using UnityEngine;
using System;

public class FieldOfView : MonoBehaviour
{
    public float patrolViewDistance;
    public float chaseViewDistance;
    public LayerMask enemyViewLayer;
    public LayerMask ghostLayer;

    private int ghostLayerInt;

    private RaycastHit2D hit;

    [Header("For Debug")]
    public Transform targetTransform;

    [HideInInspector] public Color gizmoColor;
    [HideInInspector] public bool isTargetInSight;
    [HideInInspector] public bool isChasing;
    [HideInInspector] public float scanAngle;
    [HideInInspector] public float scanDistance;
    [HideInInspector] public Vector3 right45; // For patrol scan
    [HideInInspector] public Vector3 left45; // For patrol scan

    private void Start()
    {
        scanDistance = patrolViewDistance;
        ghostLayerInt = LayerMask.NameToLayer("Ghost");
    }

    private void Update()
    {
        if (isChasing)
        {
            float dis = (targetTransform.position - transform.position).magnitude;
            if (dis > chaseViewDistance)
            { 
                isTargetInSight = false;
			}
            else 
			{ 
                isTargetInSight = true;
			}
        }
        else
        {
            hit = Physics2D.Raycast(transform.position, transform.up, scanDistance, enemyViewLayer);
            PatrolScan();
            if (hit && hit.transform.CompareTag("Player"))
            {
                isTargetInSight = hit;
                targetTransform = hit.transform;
            }
        }

    }

    private void PatrolScan()
    {
        // Scan 90 degrees
        if (Quaternion.Angle(Quaternion.Euler(transform.up), Quaternion.Euler(left45)) <= 0.01f)
        {
            transform.Rotate(0, 0, -90);
            return;
		}
        else
        {
            transform.Rotate(0, 0, 90 * 5 * Time.deltaTime); // scan 90 degree 5 times in one sec
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = gizmoColor;
        if (isChasing)
        {
            Gizmos.DrawWireSphere(transform.position, chaseViewDistance);
		}
        if (!isChasing)
        {
            Gizmos.DrawRay(transform.position, transform.up * scanDistance);
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, left45 * scanDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, right45 * scanDistance);
        }
    }
}
