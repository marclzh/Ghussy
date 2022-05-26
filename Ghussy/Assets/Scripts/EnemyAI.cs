using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI: MonoBehaviour

{
    // Enemy References
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Vector2 movement;

    // Enemy Variables
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public bool shouldRotate;
    public Vector3 dir;
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    // Attacking Variables
    private bool isInChaseRange;
    private bool isInAttackRange;

    // Player References
    public LayerMask whatIsPlayer;
    private Transform target;
    

    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        // Initializing target for enemy as the player
        target = GameObject.FindWithTag("Player").transform;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        dir.Normalize();
        movement = dir;
    }

    private void FixedUpdate()
    {
        if (isInChaseRange && !isInAttackRange)
        {
            animator.SetBool("isMoving", isInChaseRange);
            MoveCharacter(movement);
        }

        if (isInAttackRange)
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        if (dir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else 
        {
            spriteRenderer.flipX = false;
        }
        rigidBody.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }     
    }
}
