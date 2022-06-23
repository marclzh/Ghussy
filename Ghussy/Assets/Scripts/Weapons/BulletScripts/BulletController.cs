using UnityEngine;

public abstract class BulletController : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator animator;
    public float damage;
    public float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyComponent) )
        {
            FindObjectOfType<AudioManager>().Play("Hit");
            enemyComponent.TakeDamage(damage);         
        }
        animator.SetBool("isCollided", true);
    }
}
