using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemyComponent) )
        {
            enemyComponent.TakeDamage(10);
            
        }

        animator.SetBool("isCollided", true);
    }
}
