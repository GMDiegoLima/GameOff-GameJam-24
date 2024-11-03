using UnityEngine;

public class EnemyBody : MonoBehaviour
{
    public PlayerController.Bodies tipoDeCorpo;
    private SpriteRenderer _spriteRenderer;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}