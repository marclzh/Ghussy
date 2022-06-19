using UnityEngine;

public abstract class BulletController : MonoBehaviour
{
    public Rigidbody2D rb;
    protected int damage;
    private Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyComponent) )
        {
            enemyComponent.TakeDamage(damage);         
        }
        animator.SetBool("isCollided", true);
    }

    public void setDamage(int dmg)
    {
        this.damage = dmg;
    }
}