using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public bool isTargetInSight;
    public string targetTag = "Player";
    public LayerMask targetLayer;
    public Color gizmoColor;
    [HideInInspector] public Transform targetTransform;

    public float sizeX;
    public float sizeY;
    public float sacnAngle;

    private void Awake()
    {
    }

    private void Update()
    {
        sacnAngle = transform.rotation.z;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(sizeX, 1), sacnAngle, transform.up,
                                             sizeY, targetLayer);
        isTargetInSight = hit;
        targetTransform = hit.transform; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Debug.Log(targetTag + " in sight");
            isTargetInSight = true;
            targetTransform = collision.transform;
		}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Debug.Log(targetTag + " not in sight");
            isTargetInSight = false;
            targetTransform = null;
		}
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position + transform.up * sizeY / 2, 
			                transform.parent.localScale * new Vector2(sizeX, sizeY));
        Gizmos.DrawRay(transform.position, transform.up.normalized * sizeY);
    }
}
