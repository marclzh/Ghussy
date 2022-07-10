using UnityEngine;

public abstract class BulletController : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator animator;
    public float damage;
    public float speed;
    public Vector2 initVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        initVelocity = rb.velocity;
    }

    public void Update()
    {
        // Despawns bullet if bullet has collided but not despawned
        if (rb != null)
        {
            if (rb.velocity != initVelocity)
            {
                animator.SetBool("isCollided", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            return;
        }

        if (collision.gameObject.TryGetComponent<EnemyHealth>(out EnemyHealth enemyComponent))
        {
            FindObjectOfType<AudioManager>().Play("Hit");
            enemyComponent.TakeDamage(damage);         
        }
        animator.SetBool("isCollided", true);
    }
}
