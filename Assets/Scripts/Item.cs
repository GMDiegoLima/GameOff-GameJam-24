using UnityEngine;

public class Item : MonoBehaviour
{
    public string name;
    public int weight;
    Collider2D collider;

    void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = true;
    }
}