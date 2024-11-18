using UnityEngine;

public class Stone : MonoBehaviour, IBlowable
{
    public void GetBlow()
    {
        Debug.Log("Stone get blow");
	}
}
