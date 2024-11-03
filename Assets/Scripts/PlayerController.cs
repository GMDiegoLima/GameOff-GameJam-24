using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed;
    Rigidbody2D characterBody;
    Vector2 velocity;
    Vector2 inputMovement;

    private Animator _animator;

    public enum Bodies
    {
        Main,
        Ghost,
        Wolf,
        FatBat,
        Goblin,
    }
    public Bodies currentBody = Bodies.Main;
    private Bodies availableBody = Bodies.Ghost;
    private bool canEmbody = false;

    private GameObject currentBodyObject;
    private SpriteRenderer _spriteRenderer;

    public bool alive = true;
    public GameObject gameOver;

    void Start()
    {
        velocity = new Vector2(speed, speed);
        characterBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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

            if (Input.GetKeyDown("q")) {
                if (canEmbody && currentBody == Bodies.Ghost)
                {
                    currentBody = availableBody;
                    _animator.SetTrigger("Possess");
                    _animator.SetBool("isGhost", false);
                    Debug.Log("Possessed" + availableBody);
                    Invoke("Embody", 0.5f);
                }
                else if (currentBody != Bodies.Ghost)
                {
                    currentBody = Bodies.Ghost;
                    Invoke("Disembody", 0.5f);
                    Debug.Log("Left the body");
                }
            }

        }
        else
        {
            gameOver.SetActive(true);
            inputMovement = new Vector2(0, 0);
        }
    }
    void Embody()
    {
        if (currentBodyObject != null)
        {
            transform.position = currentBodyObject.transform.position;

            SpriteRenderer enemySpriteRenderer = currentBodyObject.GetComponent<SpriteRenderer>();
            if (enemySpriteRenderer != null)
            {
                _spriteRenderer.sprite = enemySpriteRenderer.sprite;
            }
        }
        UpdateBodyState();
    }

    void Disembody()
    {
        currentBodyObject = new GameObject("BodyShell");
        currentBodyObject.transform.position = transform.position;

        SpriteRenderer shellSpriteRenderer = currentBodyObject.AddComponent<SpriteRenderer>();
        shellSpriteRenderer.sprite = _spriteRenderer.sprite;
        shellSpriteRenderer.sortingOrder = _spriteRenderer.sortingOrder - 1;

        _animator.SetBool("isGhost", true);
        _animator.SetTrigger("TransformToGhost");

        speed = 7f;
    }

    void FixedUpdate()
    {
        Vector2 delta = inputMovement * velocity * Time.deltaTime;
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }

    void UpdateBodyState()
    {
        switch (currentBody)
        {
            case Bodies.Main:
                _animator.SetTrigger("TransformToMain");
                speed = 5f;
                break;
            case Bodies.Wolf:
                _animator.SetTrigger("TransformToWolf");
                speed = 6f;
                break;

            case Bodies.FatBat:
                _animator.SetTrigger("TransformToFatBat");
                speed = 4f;
                break;

            case Bodies.Goblin:
                _animator.SetTrigger("TransformToGoblin");
                speed = 5.5f;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadBody"))
        {
            canEmbody = true;

            EnemyBody enemyBody = other.GetComponent<EnemyBody>();
            if (enemyBody != null)
            {
                availableBody = enemyBody.tipoDeCorpo;
                Debug.Log("Can embody: " + availableBody);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DeadBody"))
        {
            currentBodyObject = null;
            canEmbody = false;
            availableBody = Bodies.Ghost;
        }
    }
}
