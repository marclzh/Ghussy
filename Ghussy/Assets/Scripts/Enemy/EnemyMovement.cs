using UnityEngine;

// Enemy Movement will handle the patrolling and movement animation behaviour of the 
// enemy, instead of handling it in the pathfinding script.
public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;

    // Enemy Movement Variables
    public float speed;
    public float chaseRadius;
    public float attackRadius;
    private Vector3 dir;

    // Attacking Variables
    public bool isInChaseRange;
    public bool isInAttackRange;

    EnemyAnimator enemyAnimator;

    // private since should be following AI's target
    private Transform target;
    public LayerMask playerLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<EnemyAnimator>();
        target = GetComponent<EnemyAI>().target;
    }

    void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, chaseRadius, playerLayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, playerLayer);

        if (enemyAnimator != null)
        { 
            if (rb.velocity.x != 0 || rb.velocity.y != 0)
            {
                if (!isInAttackRange || isInChaseRange)
                {
                    enemyAnimator.IsEnemyMoving(true);
                }          
            }
        }

        
        if (target != null) 
        {
            if (isInChaseRange)
            {
                GetComponent<EnemyAI>().isChasing = true;
            }

            dir = target.position - transform.position;
        }

        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

        dir.Normalize();

        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;

            if (enemyAnimator != null)
            {
                // Trigger attack animation (Hitbox Logic handled by animator)
                enemyAnimator.EnemyAttack();
            }
           
        }
    }
}