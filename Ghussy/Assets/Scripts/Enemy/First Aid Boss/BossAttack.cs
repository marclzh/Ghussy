using UnityEngine;

public class BossAttack : MonoBehaviour
{
    // Damage of the enemy attack
    [SerializeField] private float damage;
    // Reference to electric ghost main body
    private Transform boss;

    void Start()
    {
        boss = transform.parent.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GhussyTarget"))
        {
            if (collision.gameObject.transform.parent.TryGetComponent<PlayerHealth>(out PlayerHealth playerComponent))
            {
                // Play Audio
                AudioManager.Instance.Play("BossAttack");

                // Deal Damage
                playerComponent.TakeDamage(damage);
            }
        }
    }

}
