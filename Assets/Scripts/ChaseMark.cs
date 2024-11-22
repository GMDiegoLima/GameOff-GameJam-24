using UnityEngine;

public class ChaseMark : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    public void TurnOn()
    {
        sprite.enabled = true;
	}
    
    public void TurnOff()
    {
        sprite.enabled = false;
	}
}
