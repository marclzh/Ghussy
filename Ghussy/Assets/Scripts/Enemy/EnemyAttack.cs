using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Damage of the enemy attack
    public float damage;
    // Animator Reference
    private EnemyAnimator enemyAnimation;
    // Boolean to check if player is in attacking range
    private bool inAttackRange;
    // Layer of the player
    private LayerMask playerLayer;
    //private bool isAttacking;

    public Transform attackPoint;

    void Start()
    {
        enemyAnimation = GetComponent<EnemyAnimator>();
    }

    void Update()
    {
        inAttackRange = GetComponent<EnemyMovement>().isInAttackRange;
    }

    private void FixedUpdate()
    {
        if (inAttackRange)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Trigger attack animation (Hitbox Logic handled by animator)
        enemyAnimation.EnemyAttack();
       
    }
}
