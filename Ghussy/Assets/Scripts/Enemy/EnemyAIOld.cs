using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIOld : MonoBehaviour

{
    // Enemy References
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Vector2 movement;
    public EnemyHealth enemyHealth;

    // Enemy Variables
    public float speed;
    public float checkRadius;
    public float attackRadius;
    public bool shouldRotate;
    public Vector3 dir;
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    public float reachedPos;


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
        // Patrol var
        startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
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
        /*
        if (!isInChaseRange && !isInAttackRange)
        {
            animator.SetBool("isMoving", true);
            MoveCharacter(roamPosition);
            if (Vector3.Distance(transform.position, roamPosition) < reachedPos)
            {
                // reached pos
                roamPosition = GetRoamingPosition();
            }
        }
        */
        

        if (isInChaseRange && !isInAttackRange)
        {
            animator.SetBool("isMoving", isInChaseRange);
            MoveCharacter(movement);
        }

        if (isInAttackRange)
        {
            rigidBody.velocity = Vector2.zero;
            animator.SetBool("isAttacking", true);
        } else
        {
            animator.SetBool("isAttacking", false);
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

    private Vector3 GetRoamingPosition()
    {
        Vector3 randDir =  new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        return startingPosition + randDir * Random.Range(0.5f, 0.5f);
    }

    public void OnHit(float damage)
    {
        enemyHealth.TakeDamage(damage);
    }
}
