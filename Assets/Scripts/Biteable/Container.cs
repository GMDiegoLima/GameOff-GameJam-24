using UnityEngine;

public class Container : MonoBehaviour, IBiteable
{
    [SerializeField] private GameObject itemInside;

    public void GetBit()
    {
        Debug.Log("Conainer get bite");
        Instantiate(itemInside, transform.position, Quaternion.identity);
        Destroy(gameObject);
	}
}
