using UnityEngine;

public class QuestionMark : MonoBehaviour
{
    private SpriteRenderer sprite;
    [HideInInspector] public bool isTurnedOn;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    public void TurnOn()
    {
        sprite.enabled = true;
        isTurnedOn = true;
	}
    
    public void TurnOff()
    {
        sprite.enabled = false;
        isTurnedOn = false;
	}
}
