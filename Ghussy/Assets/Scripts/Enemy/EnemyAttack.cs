using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Damage of the enemy attack
    [SerializeField] private float damage;
    // Animator Reference
    private EnemyAnimator enemyAnimation;
    // Boolean to check if player is in attacking range
    private bool inAttackRange;
    // Reference to electric ghost main body
    private Transform eGhost;

    void Start()
    {
        eGhost = transform.parent.parent.transform;
        enemyAnimation = eGhost.GetComponent<EnemyAnimator>();
    }

    void Update()
    {
        inAttackRange = eGhost.GetComponent<EnemyMovement>().isInAttackRange;
    }

    private void FixedUpdate()
    {
        if (inAttackRange)
        {
            // Trigger attack animation (Hitbox Logic handled by animator)
            enemyAnimation.EnemyAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GhussyTarget"))
        {
            if (collision.gameObject.transform.parent.TryGetComponent<PlayerHealth>(out PlayerHealth playerComponent))
            {
                playerComponent.TakeDamage(damage);
            }
        }

        
    }

}
