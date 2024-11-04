using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed;
    Rigidbody2D characterBody;
    Vector2 velocity;
    Vector2 inputMovement;

    private Animator _animator;
    private RuntimeAnimatorController originalAnimatorController;

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

    private GameObject enemyBodyPrefab;
    private SpriteRenderer _spriteRenderer;
    private bool isMovingToTarget = false;
    private Vector3 targetPosition;

    public bool alive = true;
    public GameObject gameOver;

    void Start()
    {
        _animator = GetComponent<Animator>();
        velocity = new Vector2(speed, speed);
        characterBody = GetComponent<Rigidbody2D>();
        originalAnimatorController = _animator.runtimeAnimatorController; 
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

            if (isMovingToTarget)
            {
                MoveToTarget();
                return;
            }

            if (Input.GetKeyDown("q")) {
                if (canEmbody && currentBody == Bodies.Ghost)
                {
                    currentBody = availableBody;
                    targetPosition = enemyBodyPrefab.transform.position;
                    // _animator.SetTrigger("Embody");
                    isMovingToTarget = true;
                    Debug.Log("Possessed " + availableBody);
                }
                else if (currentBody != Bodies.Ghost)
                {
                    enemyBodyPrefab = new GameObject("BodyShell");
                    enemyBodyPrefab.transform.position = transform.position;
                    enemyBodyPrefab.transform.localScale = transform.localScale;

                    SpriteRenderer shellSpriteRenderer = enemyBodyPrefab.AddComponent<SpriteRenderer>();
                    
                    shellSpriteRenderer.sprite = _spriteRenderer.sprite;
                    shellSpriteRenderer.sortingOrder = _spriteRenderer.sortingOrder - 1;
                    _animator.SetTrigger("Disembody");
                    Invoke("Disembody", 0.44f);
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
    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMovingToTarget = false;
            _animator.SetBool("isGhost", false);
            // Invoke("Embody", 0.4f);
            Embody();
        }
    }

    void Embody()
    {
        if (enemyBodyPrefab != null)
        {
            transform.position = enemyBodyPrefab.transform.position;

            SpriteRenderer enemySpriteRenderer = enemyBodyPrefab.GetComponent<SpriteRenderer>();
            if (enemySpriteRenderer != null)
            {
                _spriteRenderer.sprite = enemySpriteRenderer.sprite;
            }

            Animator bodyShellAnimator = enemyBodyPrefab.GetComponent<Animator>();
            _animator.runtimeAnimatorController = bodyShellAnimator.runtimeAnimatorController;

            Destroy(enemyBodyPrefab);
        }
        switch (currentBody)
        {
            case Bodies.Main:
                speed = 5f;
                break;
            case Bodies.Wolf:
                speed = 6f;
                break;
            case Bodies.FatBat:
                speed = 4f;
                break;
            case Bodies.Goblin:
                speed = 5.5f;
                break;
        }
    }

    void Disembody()
    {
        CircleCollider2D collider = enemyBodyPrefab.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = 1.5f;
        enemyBodyPrefab.tag = "DeadBody";

        EnemyBody enemyBodyComponent = enemyBodyPrefab.AddComponent<EnemyBody>();
        enemyBodyComponent.bodyType = currentBody;
        currentBody = Bodies.Ghost;

        Animator shellAnimator = enemyBodyPrefab.AddComponent<Animator>();
        shellAnimator.runtimeAnimatorController = _animator.runtimeAnimatorController;
        
        _animator.runtimeAnimatorController = originalAnimatorController;
        _animator.SetBool("isGhost", true);

        speed = 7f;
    }

    void FixedUpdate()
    {
        Vector2 delta = inputMovement * velocity * Time.deltaTime;
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadBody"))
        {
            canEmbody = true;

            EnemyBody enemyBody = other.GetComponent<EnemyBody>();
            if (enemyBody != null)
            {
                availableBody = enemyBody.bodyType;
                enemyBodyPrefab = enemyBody.gameObject;
                Debug.Log("Can embody: " + availableBody);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DeadBody"))
        {
            enemyBodyPrefab = null;
            canEmbody = false;
            availableBody = Bodies.Ghost;
            Debug.Log("Cannot embody anymore");
        }
    }
}
