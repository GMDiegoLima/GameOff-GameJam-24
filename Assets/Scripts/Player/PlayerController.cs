using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private KeyCode embodyKey;
    [SerializeField] private KeyCode pickKey;
    [SerializeField] private KeyCode dropKey;

    private PlayerStateController playerStateController;
    private GameActorSOHolder gameActorSOHolder;

    public float speed;
    Rigidbody2D characterBody;
    Vector2 velocity;
    Vector2 direction;

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

    public GameObject heldItem = null;
    public Transform itemHoldPosition;
    List<GameObject> nearbyItems = new List<GameObject>();

    void Start()
    {
        _animator = GetComponent<Animator>();
        velocity = new Vector2(speed, speed);
        characterBody = GetComponent<Rigidbody2D>();
        originalAnimatorController = _animator.runtimeAnimatorController; 
        _spriteRenderer = GetComponent<SpriteRenderer>();
        playerStateController = GetComponent<PlayerStateController>();
        gameActorSOHolder = GetComponent<GameActorSOHolder>();
    }

    void Update()
    {
        if (alive)
        {
            direction = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
                ).normalized;
            _animator.SetFloat("Horizontal", direction.x);
            _animator.SetFloat("Vertical", direction.y);

            if (direction != Vector2.zero)
            {
                _animator.SetFloat("LastHorizontal", direction.x);
                _animator.SetFloat("LastVertical", direction.y);
            }

            if (isMovingToTarget)
            {
                MoveToTarget();
                return;
            }

            if (Input.GetKeyDown(embodyKey)) {
                if (canEmbody && currentBody == Bodies.Ghost)
                {
                    currentBody = availableBody;
                    targetPosition = enemyBodyPrefab.transform.position;
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
            if (Input.GetKeyDown(pickKey) && nearbyItems.Count > 0)
            {
                GameObject closestItem = GetClosestItem();
                if (heldItem)
                {
                    DropItem();
                    PickUpItem(closestItem);
                }
                else
                {
                    PickUpItem(closestItem);
                }

            }
            if (Input.GetKeyDown(dropKey) && heldItem != null)
            {
                DropItem();
            }
            if (heldItem != null)
            {
                heldItem.transform.position = itemHoldPosition.position;
            }
        }
        else
        {
            gameOver.SetActive(true);
            direction = new Vector2(0, 0);
        }
    }
    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            isMovingToTarget = false;
            _animator.SetBool("isGhost", false);
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
                playerStateController.actor = gameActorSOHolder.mainCharacter;
                break;
            case Bodies.Wolf:
                playerStateController.actor = gameActorSOHolder.wolf;
                break;
            case Bodies.FatBat:
                playerStateController.actor = gameActorSOHolder.fatBat;
                break;
            case Bodies.Goblin:
                playerStateController.actor = gameActorSOHolder.goblin;
                break;
            case Bodies.Skeleton:
                playerStateController.actor = gameActorSOHolder.skeleton;
                break;
        }
        UpdateActorState();
        playerStateController.UpdateActorController();
    }

    void Disembody()
    {
        if (enemyBodyPrefab != null)
        {
            EnemyBody enemyBodyComponent = enemyBodyPrefab.AddComponent<EnemyBody>();
            enemyBodyComponent.alive = false;
            enemyBodyComponent.bodyType = currentBody;
            currentBody = Bodies.Ghost;

            _animator.runtimeAnimatorController = originalAnimatorController;
            _animator.SetBool("isGhost", true);
            CircleCollider2D collider = enemyBodyPrefab.AddComponent<CircleCollider2D>();
            collider.isTrigger = true;
            collider.radius = 1.5f;

            playerStateController.actor = gameActorSOHolder.ghost;
            UpdateActorState();
        }
    }

    void UpdateActorState()
    { 
        flying = playerStateController.actor.canFlay;
        speed = playerStateController.actor.moveSpeed;
	}

    GameObject GetClosestItem()
    {
        return nearbyItems
            .OrderBy(item => Vector2.Distance(transform.position, item.transform.position))
            .FirstOrDefault();
    }
    void PickUpItem(GameObject item)
    {
        heldItem = item;
        heldItem.transform.SetParent(transform);
        heldItem.transform.position = itemHoldPosition.position;
        nearbyItems.Remove(item);
    }
    public void DropItem()
    {
        heldItem.transform.SetParent(null);
        nearbyItems.Add(heldItem);
        heldItem = null;
    }

    void FixedUpdate()
    {
        Vector2 delta = direction * velocity * Time.deltaTime;
        Vector2 newPosition = characterBody.position + delta;
        characterBody.MovePosition(newPosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PressureTrigger") && flying)
        {
            _animator.SetBool("Pushing", false);
            AkSoundEngine.ExecuteActionOnEvent("rock_push", AkActionOnEventType.AkActionOnEventType_Stop, gameObject);
            Rigidbody2D rb_rock = other.gameObject.GetComponent<Rigidbody2D>();
            rb_rock.simulated = false;
        }
        if (other.CompareTag("PressureTrigger") && !flying)
        {
            Debug.Log("Pedra");
            _animator.SetBool("Pushing", true);
            AkSoundEngine.PostEvent("rock_push", gameObject);
            Rigidbody2D rb_rock = other.gameObject.GetComponent<Rigidbody2D>();
            rb_rock.simulated = true;
        }
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
        if (other.CompareTag("Item") && currentBody != Bodies.Ghost)
        {
            nearbyItems.Add(other.gameObject);
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
        if (other.CompareTag("PressureTrigger") && !flying)
        {
            _animator.SetBool("Pushing", false);
            AkSoundEngine.ExecuteActionOnEvent("rock_push", AkActionOnEventType.AkActionOnEventType_Stop, gameObject);
        }
        if (other.CompareTag("Item"))
        {
            nearbyItems.Remove(other.gameObject);
        }
    }
}