using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public bool isTargetInSight;
    public string targetTag = "Player";
    public LayerMask targetLayer;
    public Color gizmoColor;
    [HideInInspector] public Transform targetTransform;

    [HideInInspector] public float sizeX;
    [HideInInspector] public float sizeY;
    [HideInInspector] public float scanDistance;

    [Header("Patrol Range")]
    public float patrolViewSizeX;
    [HideInInspector] public float patrolViewSizeY = 1; // Normally just set to 1 
    public float patrolViewDistance;

    [Header("Chase Range (From Center)")]
    public float chaseViewSizeX;
    public float chaseViewSizeY;
    
    private void Start()
    {
        sizeX = patrolViewSizeX;       
        sizeY = patrolViewSizeY;
        scanDistance = patrolViewDistance;
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(sizeX, sizeY), 0, transform.up,
                                             scanDistance, targetLayer);
        isTargetInSight = hit;
        targetTransform = hit.transform; 
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position + transform.up * (scanDistance / 2), 
			                transform.parent.localScale * new Vector2(sizeX, sizeY * scanDistance + sizeY));
        //Gizmos.DrawRay(transform.position, transform.up.normalized * scanDistance);
    }
}
