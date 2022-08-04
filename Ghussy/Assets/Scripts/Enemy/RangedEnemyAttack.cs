using UnityEngine;

public class RangedEnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform projectilePrefab;
    [SerializeField] private float fireRateDelay = .5f;
    [SerializeField] private float lastFireTime = 0f;
    [SerializeField] private Transform playerTarget;

    

    public void Update()
    {
        // Rotate Fire Point to face Player
        Vector2 shootingDirection = playerTarget.position - firePoint.position;
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Try to Fire 
        RangeAttack();
    }

    public void RangeAttack()
    {
        Vector2 shootingDirection = playerTarget.position - firePoint.position;

        if (Time.time > (lastFireTime + fireRateDelay) || lastFireTime <= 0)
        {
            Transform projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation, transform);
            //projectile.GetComponent<Rigidbody2D>().velocity += projectile.GetComponent<WispBullet>().speed * Time.deltaTime * shootingDirection.normalized;
            projectile.GetComponent<Rigidbody2D>().AddForce(projectile.GetComponent<WispBullet>().speed * Time.deltaTime * shootingDirection.normalized
                , ForceMode2D.Impulse);
            lastFireTime = Time.time;

            // Audio Cue
            AudioManager.Instance.Play("WispAttack");
        }
    }
}
