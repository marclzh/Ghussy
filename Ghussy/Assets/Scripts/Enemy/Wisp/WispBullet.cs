using UnityEngine;

public class WispBullet : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private Animator animator;

    // Bullet Fields
    [SerializeField] private float damage;
    [SerializeField] public float speed;
    private Vector2 initVelocity;

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

        transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, 0f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            return;
        }

        if (collision.CompareTag("CollisionObjects"))
        {
            animator.SetBool("isCollided", true);
            Destroy(transform.gameObject, 0.2f);
        }

        if (collision.CompareTag("GhussyTarget"))
        {

            if (collision.gameObject.transform.parent.TryGetComponent<PlayerHealth>(out PlayerHealth playerComponent))
            {

                playerComponent.TakeDamage(damage);

                animator.SetBool("isCollided", true);
            }
        }

        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
