using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Vine : MonoBehaviour, ICuttable
{
    private BoxCollider2D boxCollider; 
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void GetCut()
    {
        sprite.color = Color.gray; // For test
        // Add animation and sfx
        boxCollider.enabled = false;
        Debug.Log("Vine get cut");
	}
}
