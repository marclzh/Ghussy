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
        Debug.Log(collision);
        Debug.Log(collision.gameObject);
       
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyComponent) )
        {
            enemyComponent.TakeDamage(20);         
        }

        Debug.Log(animator);

        animator.SetBool("isCollided", true);
    }
}
