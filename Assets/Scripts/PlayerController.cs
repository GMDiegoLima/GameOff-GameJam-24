using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool alive = true;
    private Animator _animator;
    public float speed;
    Rigidbody2D characterBody;
    Vector2 velocity;
    Vector2 inputMovement;

    public GameObject gameOver;

    void Start()
    {
        velocity = new Vector2(speed, speed);
        characterBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (alive)
        {
            inputMovement = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
                );
            _animator.SetFloat("Horizontal", inputMovement.x);
            _animator.SetFloat("Vertical", inputMovement.y);

            if (inputMovement != Vector2.zero)
            {
                _animator.SetFloat("LastHorizontal", inputMovement.x);
                _animator.SetFloat("LastVertical", inputMovement.y);
            }
        }
        else
        {
            gameOver.SetActive(true);
            inputMovement = new Vector2(0, 0);
        }
    }

    void FixedUpdate()
    {
        Vector2 delta = inputMovement * velocity * Time.deltaTime;
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }
}
