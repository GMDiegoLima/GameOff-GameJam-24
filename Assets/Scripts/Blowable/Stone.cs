using UnityEngine;

public class Stone : MonoBehaviour, IBlowable
{
    Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void GetBlow(Vector2 dir, float force)
    {
        Debug.Log("Stone get blow");
        body.AddForce(dir * force, ForceMode2D.Impulse);
	}
}
