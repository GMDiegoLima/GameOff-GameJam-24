using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public PlayerController.Bodies tipoDeCorpo;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}