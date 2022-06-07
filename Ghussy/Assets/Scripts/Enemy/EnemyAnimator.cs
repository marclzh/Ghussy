using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    // Reference to the enemy's animator.
    public Animator animator;
    
    // Current animation state of the enemy.
    protected string currState;


    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void IsEnemyMoving(bool b)
    {
        animator.SetBool("isMoving", b);
    }

    public void IsEnemyAttacking(bool b)
    {
        animator.SetBool("isFiring", b);
    }
    public void EnemyHit()
    {
        animator.Play("Flinch");
    }

    public void EnemyDeath()
    {
        animator.SetBool("isDead", true);
    }
}
