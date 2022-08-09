using UnityEngine;
using System.Collections.Generic;

/**
 * Class to control the ranged enemy attack.
 */
public class RangedEnemyAttack : MonoBehaviour
{
    // Reference to the firepoint of the ranged enemy.
    [SerializeField] private Transform firePoint;
    // Reference to the prefab of the projectile.
    [SerializeField] private Transform projectilePrefab;
    // Variables controlling the attack of the ranged enemy.
    [SerializeField] private float fireRateDelay = .1f;
    [SerializeField] private float lastFireTime = 0f;
    // Reference to the player target.
    [SerializeField] private Transform playerTarget;
    // Boolean to check if the enemy is attacking.
    [SerializeField] private bool isAttacking;
    // Distance of collision detection.
    [SerializeField] private float collisionOffset = .01f;
    // Determines where a collision can occur (layers).
    [SerializeField] private ContactFilter2D collisionFilter;
    // List of collisions found during raycast.
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); 

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("GhussyTarget").transform;       
    }

    public void Update()
    {       
        // Rotate Fire Point to face Player
        Vector2 shootingDirection = playerTarget.position - firePoint.position;
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        firePoint.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Try to Fire 
        isAttacking = GetComponent<EnemyMovement>().isInAttackRange;

        if (isAttacking && TryShoot(shootingDirection))
        {
            RangeAttack();
        }
        
    }

    // Method to control the attack of the ranged enemy.
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

    // Method for the firing of the ranged enemy.  
    private bool TryShoot(Vector2 direction)
    {
        if (direction != Vector2.zero) // null check
        {
            Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();

            // Check for potential collisions
            int count = rb.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                collisionFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                rb.velocity.magnitude * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            // If no collision, move in direction and return true, else return false
            if (count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }      
        // Can't shoot if there's no direction to move in
        return false;    
    }
}
