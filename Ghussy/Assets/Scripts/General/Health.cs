using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
    // Health Stats
    protected float maxHealth;
    protected float currentHealth;
    protected bool isInvincible;

    public virtual void TakeDamage(float damage)
    {
        Debug.Assert(damage >= 0, "Damage cannot be negative!");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public void InvincibilityToggle(bool invincible)
    {
        isInvincible = invincible;
    }
}
