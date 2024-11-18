using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Rope : MonoBehaviour, ICuttable
{
    private BoxCollider2D boxCollider; 

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }

    public void GetCut()
    {
        Debug.Log("Rope get cut");
	}
}
