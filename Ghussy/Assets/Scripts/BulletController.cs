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
        if (collision.gameObject.tag == "CollisionObjects")
        {
            Debug.Log("Collided w/ CollisionObj");
            animator.SetBool("isCollided", true);
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collided w/ Enemy");
            animator.SetBool("isCollided", true);
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collided w/ Player");
            animator.SetBool("isCollided", true);
        }

       

    }
}
