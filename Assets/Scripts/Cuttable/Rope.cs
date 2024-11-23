using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class Rope : MonoBehaviour, ICuttable
{
    public GameObject cutRope;
    BoxCollider2D boxCollider;
    SpriteRenderer sprite;
    public UnityEvent onRopeCut;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        cutRope.SetActive(false);
    }

    public void GetCut()
    {
        sprite.enabled = false;
        boxCollider.enabled = false;
        cutRope.SetActive(true);
        Debug.Log("Rope get cut");
        onRopeCut.Invoke();
	}
}
