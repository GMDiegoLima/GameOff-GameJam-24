using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Rope : MonoBehaviour, ICuttable
{
    private BoxCollider2D boxCollider;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    public void GetCut()
    {
        sprite.color = Color.gray; // For test
        // Add animation and sfx
        Debug.Log("Rope get cut");
	}
}
