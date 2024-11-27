using UnityEngine;
using System.Collections.Generic;

public class FinalBoss : MonoBehaviour
{
    private Vector2 velocity;
    private Animator anim;
    private Rigidbody2D body;
    private Health health;
    [SerializeField] List<RuntimeAnimatorController> animControllers;

    [SerializeField] float changeOutfitCD;
    [SerializeField] float changeOutfitCDTimer;

    [Header("Adjustable")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float minMovePeriod;
    [SerializeField] private float maxMovePeriod;
    [SerializeField] private float minStopPeriod;
    [SerializeField] private float maxStopPeriod;
    [SerializeField] private float minVelocity;
    [SerializeField] private float maxVelocity;

    [Header("For Dubug")]
    [SerializeField] private bool isMoving;
    [SerializeField] private float movePeriodTimer;
    [SerializeField] private float stopPeriodTimer;
    [SerializeField] private float attackCDTimer;

    float movePeriod = 2f;
    float stopPeriod = 2f;
    bool isVelocityUpdated;
    bool isMovePeriodUpdated;
    bool isStopPeriodUpdated;
    bool isAlive;

    void Awake()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.maxHealth = maxHealth;
        isAlive = true;
    }

    void FixedUpdate()
    {
        Vector2 delta = velocity * Time.deltaTime;
        Vector2 newPosition = body.position + delta;
        body.MovePosition(newPosition);
    }

    void Update()
    {
        if (BossRoomController.Instance.isGameStart && isAlive)
        {
            Patrol();
            changeOutfitCDTimer += Time.deltaTime;
            if (changeOutfitCDTimer > changeOutfitCD)
            {
                ChangeOutfit();
                changeOutfitCDTimer = 0;
            }
            HandleAnim();
            CheckHealth();
        }
    }

    public void Patrol()
    {
        // Randomly set move period, stop period, and velocity
        if (isMoving)
        {
            movePeriodTimer += Time.deltaTime;
            if (!isVelocityUpdated)
            {
                SetRandomVelocity();
                isVelocityUpdated = true;
            }
            if (!isMovePeriodUpdated)
            {
                SetRandomMovePeriod();
                isMovePeriodUpdated = true;
            }
            if (movePeriodTimer >= movePeriod)
            {
                isMoving = false;
                isVelocityUpdated = false;
                isMovePeriodUpdated = false;
                movePeriodTimer = 0;
            }
        }
        else
        {
            stopPeriodTimer += Time.deltaTime;
            velocity = Vector2.zero;
            if (!isStopPeriodUpdated)
            {
                SetRandomStopPeriod();
                isStopPeriodUpdated = true;
            }
            if (stopPeriodTimer >= stopPeriod)
            {
                isMoving = true;
                isStopPeriodUpdated = false;
                stopPeriodTimer = 0;
            }
        }
    }

    public void ChangeOutfit()
    {
        Debug.Log("Change Outfit");
        int idx = Random.Range(0, animControllers.Count);
        anim.runtimeAnimatorController = animControllers[idx];
	}

    void SetRandomVelocity()
    {
        float x = Random.Range(minVelocity, maxVelocity);
        float y = Random.Range(minVelocity, maxVelocity);
        movePeriodTimer = 0;
        velocity = new Vector2(x, y);
    }

    void SetRandomMovePeriod()
    {
        movePeriod = Random.Range(minMovePeriod, maxMovePeriod);
    }

    void SetRandomStopPeriod()
    {
        stopPeriod = Random.Range(minStopPeriod, maxStopPeriod);
    }

    void HandleAnim()
    { 
        Vector2 dir = velocity.normalized;
        anim.SetFloat("Horizontal", dir.x);
        anim.SetFloat("Vertical", dir.y);
        if (dir!= Vector2.zero)
        {
            anim.SetFloat("LastHorizontal", dir.x);
            anim.SetFloat("LastVertical", dir.y);
        }
	}

    void CheckHealth()
    {
        if (health.isDead)
        {
            Debug.Log("Boss Died");
            isAlive = false;
            velocity = Vector2.zero;
            anim.SetBool("Dead", true);
        }
	}
}
