using UnityEngine;

public class Rope : MonoBehaviour, ICuttable
{
    public void GetCut()
    {
        Debug.Log("Rope get cut");
	}
}
