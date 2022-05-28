using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Health : MonoBehaviour
{
    // Health Stats
    protected float maxHealth;
    protected float currentHealth;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public virtual void TakeDamage(float damage)
    {
        Debug.Assert(damage >= 0, "Damage cannot be negative!");
        currentHealth -= damage;
        UpdateHealthUI(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    protected virtual void UpdateHealthUI(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }



}
