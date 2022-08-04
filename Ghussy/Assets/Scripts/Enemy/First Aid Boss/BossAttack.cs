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

    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.parent.TryGetComponent<PlayerHealth>(out PlayerHealth playerComponent))
        {
            playerComponent.TakeDamage(damage);
        }
    }

}
