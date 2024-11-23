using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Vine : MonoBehaviour, ICuttable
{
    BoxCollider2D boxCollider; 

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void GetCut()
    {
        Debug.Log("Vine get cut");
        Destroy(gameObject);
	}
}
