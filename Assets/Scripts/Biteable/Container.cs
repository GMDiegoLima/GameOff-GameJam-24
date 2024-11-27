using UnityEngine;

public class Container : MonoBehaviour, IBiteable
{
    [SerializeField] private GameObject itemInside;

    public void GetBit()
    {
        Debug.Log("Conainer get bite");
        GameObject spawnedItem = Instantiate(itemInside, transform.position, Quaternion.identity);
        AkSoundEngine.PostEvent("destroy", gameObject);
        switch (itemInside.name)
        {
            case "bone":
            {
                spawnedItem.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
            }
            case "Apple":
            {
                spawnedItem.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                break;
            }
            case "emerald":
            {
                spawnedItem.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                break;
            }
            default:
                spawnedItem.transform.localScale = new Vector3(1f, 1f, 1f);
                break;
        }
        
        Destroy(gameObject);
	}
}
