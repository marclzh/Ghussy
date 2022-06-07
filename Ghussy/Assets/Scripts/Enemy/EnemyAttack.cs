using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    protected const string ENEMY_ATTACK = "Attack";

    // Delay between enemy attacks
    public float attackDelay;
    // Damage of the enemy attack
    public float damage;
    // Animator Reference
    private EnemyAnimator enemyAnimation;
    // Boolean to check if player is in attacking range
    private bool inAttackRange;
    // Layer of the player
    private LayerMask playerLayer;
    // Boolean for attack cooldown
    private bool coolDown;
    // timer for attack cooldown
    [SerializeField] private float timer;
    [SerializeField] private float attackLength;
    private bool isAttacking;

    void Start()
    {
        timer = attackDelay;
        enemyAnimation = GetComponent<EnemyAnimator>();
    }

    void Update()
    {
        inAttackRange = GetComponent<EnemyMovement>().isInAttackRange;
    }

    private void FixedUpdate()
    {
        if (inAttackRange && !coolDown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Reset timer everytime method is called.
        timer = attackDelay;

        isAttacking = true;

        // Trigger attack animation
        enemyAnimation.IsEnemyAttacking(true);

        Invoke("triggerCoolDown", attackLength);
    }

    private void attackComplete()
    {
        isAttacking = false;

        enemyAnimation.IsEnemyAttacking(false);
    }
}
