using UnityEngine;

/**
 * Class to control the enemy's animator.
 */
public class EnemyAnimator : MonoBehaviour
{
    // Reference to the enemy's animator.
    public Animator animator;
    
    // Current animation state of the enemy.
    protected string currState;

    // Method to control the movement animation of the enmy.
    public void IsEnemyMoving(bool b)
    {
        animator.SetBool("isMoving", b);
    }

    // Method to control the attacking animation of the enemy.
    public void EnemyAttack()
    {
        animator.SetTrigger("Attack");
    }

    // Method to stop the attacking animation of the enemy.
    public void EnemyStopAttack()
    {
        animator.ResetTrigger("Attack");
    }

    // Method to play the flinching animation of the enemy.
    public virtual void EnemyHit()
    {
        animator.Play("Flinch");
    }

    // Method to play the enemy death animation.
    public void EnemyDeath()
    {
        animator.SetBool("isDead", true);
    }

    // Method for the Boss enrage animation.
    public void BossEnraged()
    {
        animator.SetBool("IsEnraged", true);
    }
}
