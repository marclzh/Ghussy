using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D rb;
    Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyComponent) )
        {
            enemyComponent.TakeDamage(20);         
        }

        animator.SetBool("isCollided", true);
    }
}
