using UnityEngine;

/**
 * This class contains the logic for bullets instantiated.
 */
public abstract class BulletController : MonoBehaviour
{
    // Projectile References
    public Rigidbody2D rb;
    private Animator animator;

    // Projectile Variables
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

        if (collision.gameObject.TryGetComponent<BossHealth>(out BossHealth bossComponent))
        {
            FindObjectOfType<AudioManager>().Play("Hit");
            bossComponent.TakeDamage(damage);
        }

        animator.SetBool("isCollided", true);
    }

    // Destroys the projectile upon leaving the main camera
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
