using UnityEngine;

// Equip owned items when player change scene
public class EquipmentManager : MonoBehaviour
{
    public Transform hatPosition;
    public Transform glassesPosition;

    public GameObject[] allHats;
    public GameObject[] allGlasses;

    void Start()
    {
        EquipOwnedItems();
    }

    void EquipOwnedItems()
    {
        foreach (string hatName in GlobalManager.Instance.ownedHats)
        {
            GameObject hat = FindItemByName(allHats, hatName);
            if (hat != null)
            {
                GameObject instantiatedHat = Instantiate(hat, hatPosition.position, Quaternion.identity);

                instantiatedHat.transform.SetParent(hatPosition);
                instantiatedHat.transform.localPosition = Vector3.zero;
                break;
            }
        }

        foreach (string glassesName in GlobalManager.Instance.ownedGlasses)
        {
            GameObject glasses = FindItemByName(allGlasses, glassesName);
            if (glasses != null)
            {
                GameObject instantiatedGlasses = Instantiate(glasses, glassesPosition.position, Quaternion.identity);

                instantiatedGlasses.transform.SetParent(glassesPosition);
                instantiatedGlasses.transform.localPosition = Vector3.zero;
                break;
            }
        }
    }

    GameObject FindItemByName(GameObject[] items, string name)
    {
        foreach (GameObject item in items)
        {
            if (item.name == name)
                return item;
        }
        return null;
    }
}
