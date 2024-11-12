using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float patrolViewDistance;
    public float chaseViewDistance;
    public LayerMask enemyViewLayer;

    private RaycastHit2D hit;

    [HideInInspector] public Color gizmoColor;
    [HideInInspector] public bool isTargetInSight;
    [HideInInspector] public Transform targetTransform;
    [HideInInspector] public float scanAngle;
    [HideInInspector] public float scanDistance;
    [HideInInspector] public bool isChasing;
    [HideInInspector] public Vector3 right45;
    [HideInInspector] public Vector3 left45;

    private void Start()
    {
        scanDistance = patrolViewDistance;
    }

    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, transform.up, scanDistance, enemyViewLayer);
        if (hit && hit.transform.CompareTag("Player"))
        {
            isTargetInSight = hit;
            targetTransform = hit.transform;
        }
        else 
		{
            isTargetInSight = false;
            targetTransform = null;
		}
        HandleScan();
    }

    private void HandleScan()
    {
        if (!isChasing) PatrolScan();
        else ChaseScan();
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

    private void ChaseScan()
    {
        // Scan 360 degrees
        transform.Rotate(0, 0, 360 * 5 * Time.deltaTime); // scan 360 degree 5 times in one sec
    }


    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = gizmoColor;
        Gizmos.DrawRay(transform.position, transform.up * scanDistance);
        if (!isChasing)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, left45 * scanDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, right45 * scanDistance);
        }
    }
}
