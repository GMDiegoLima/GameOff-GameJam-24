using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStateController playerStateController;

    public float speed;
    Rigidbody2D characterBody;
    Vector2 velocity;
    Vector2 inputMovement;

    Animator _animator;
    RuntimeAnimatorController originalAnimatorController;

    public enum Bodies
    {
        Main,
        Ghost,
        Wolf,
        FatBat,
        Goblin,
        Skeleton
    }
    public Bodies currentBody = Bodies.Main;
    public TextMeshProUGUI embodyText;
    Bodies availableBody = Bodies.Ghost;
    bool canEmbody = false;

    GameObject enemyBodyPrefab;
    SpriteRenderer _spriteRenderer;
    bool isMovingToTarget = false;
    Vector3 targetPosition;

    public bool alive = true;
    public bool flying = false;
    public GameObject gameOver;

    void Start()
    {
        _animator = GetComponent<Animator>();
        velocity = new Vector2(speed, speed);
        characterBody = GetComponent<Rigidbody2D>();
        originalAnimatorController = _animator.runtimeAnimatorController;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        playerStateController = GetComponent<PlayerStateController>();
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

            if (Input.GetKeyDown("q"))
            {
                if (canEmbody && currentBody == Bodies.Ghost)
                {
                    currentBody = availableBody;
                    targetPosition = enemyBodyPrefab.transform.position;
                    // _animator.SetTrigger("Embody");
                    isMovingToTarget = true;
                    _animator.Play("Embody");
                    Debug.Log("Possessed " + availableBody);
                }
                else if (currentBody != Bodies.Ghost && enemyBodyPrefab == null)
                {
                    enemyBodyPrefab = new GameObject(currentBody + " body");
                    enemyBodyPrefab.tag = "DeadBody";

                    enemyBodyPrefab.transform.position = transform.position;
                    enemyBodyPrefab.transform.localScale = transform.localScale;

                    SpriteRenderer shellSpriteRenderer = enemyBodyPrefab.AddComponent<SpriteRenderer>();
                    shellSpriteRenderer.sprite = _spriteRenderer.sprite;
                    shellSpriteRenderer.sortingOrder = _spriteRenderer.sortingOrder - 1;

                    Animator shellAnimator = enemyBodyPrefab.AddComponent<Animator>();
                    shellAnimator.runtimeAnimatorController = _animator.runtimeAnimatorController;
                    shellAnimator.SetBool("Dead", true);
                    _animator.Play("Disembody");
                    flying = true;
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
            Animator bodyShellAnimator = enemyBodyPrefab.GetComponent<Animator>();
            _animator.runtimeAnimatorController = bodyShellAnimator.runtimeAnimatorController;
            transform.position = enemyBodyPrefab.transform.position;

            SpriteRenderer enemySpriteRenderer = enemyBodyPrefab.GetComponent<SpriteRenderer>();
            if (enemySpriteRenderer != null)
            {
                _spriteRenderer.sprite = enemySpriteRenderer.sprite;
            }

            Destroy(enemyBodyPrefab);
        }
        switch (currentBody)
        {
            case Bodies.Main:
                speed = 5f;
                flying = false;
                gameObject.AddComponent<MainCharacter>();
                break;
            case Bodies.Wolf:
                speed = 6f;
                flying = false;
                gameObject.AddComponent<Wolf>();
                break;
            case Bodies.FatBat:
                speed = 4f;
                flying = true;
                gameObject.AddComponent<FatBat>();
                break;
            case Bodies.Goblin:
                speed = 5.5f;
                flying = false;
                break;
            case Bodies.Skeleton:
                speed = 3f;
                flying = false;
                break;
        }
        playerStateController.UpdateCurrentActor(GetComponent<GameActor>());
    }

    void Disembody()
    {
        if (enemyBodyPrefab != null)
        {
            EnemyBody enemyBodyComponent = enemyBodyPrefab.AddComponent<EnemyBody>();
            enemyBodyComponent.bodyType = currentBody;
            currentBody = Bodies.Ghost;

            _animator.runtimeAnimatorController = originalAnimatorController;
            _animator.SetBool("isGhost", true);
            CircleCollider2D collider = enemyBodyPrefab.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
            collider.radius = 1.5f;

            speed = 7f;

            playerStateController.RemoveCurrentActor();
        }
    }

    void FixedUpdate()
    {
        Vector2 delta = inputMovement * velocity * Time.deltaTime;
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DeadBody") && currentBody == Bodies.Ghost)
        {
            embodyText.enabled = true;
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

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DeadBody") && other.gameObject == enemyBodyPrefab)
        {
            embodyText.enabled = false;
            enemyBodyPrefab = null;
            canEmbody = false;
            availableBody = Bodies.Ghost;
            Debug.Log("Cannot embody anymore");
        }
    }
}
