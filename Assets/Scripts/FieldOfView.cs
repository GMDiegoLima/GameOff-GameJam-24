using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public bool isTargetInSight;
    public string targetTag = "Player";
    public Color gizmoColor;
    [HideInInspector] public Transform targetTransform;

    public float sizeX;
    public float sizeY;
    private float offsetX;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        boxCollider.offset = new Vector2(offsetX, boxCollider.offset.y);          
        boxCollider.size = new Vector2(sizeX, sizeY);
        offsetX = sizeX / 2;
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
        Gizmos.DrawWireCube((Vector2)transform.position + boxCollider.offset, transform.parent.localScale * boxCollider.size);
    }
}
