using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneWeapon : Weapon
{
    public Ability ability;

    // Ability References
    private int numProjectiles;
    private float projectileSpread;

    public void BoneShotgun(Vector2 shootingDirection)
    {
        projectileSpread = 35;
        numProjectiles = 3;

        float facingRotation = Mathf.Atan2(firePoint.transform.position.x,
           firePoint.transform.position.y) * Mathf.Rad2Deg;
        //weapon.GetComponent<Weapon>().firePoint.rotation;
        float startRotation = facingRotation + projectileSpread / 2f;
        float angleIncrease = projectileSpread / ((float)numProjectiles - 1f);

        for (int i = 0; i < numProjectiles; i++)
        {
            float tempRot = startRotation + angleIncrease * i;
            GameObject projectile = Instantiate(bulletPrefab, firePoint.position,
                Quaternion.Euler(0f, 0f, tempRot));
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity += bulletPrefab.GetComponent<BulletController>().speed * Time.deltaTime * shootingDirection.normalized;
        }
        FindObjectOfType<AudioManager>().Play("BoneWeapon");
    }
}
