using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    Animator animator;

    private void Start()
    {
        // Initialise Health
        maxHealth = 100;
        currentHealth = maxHealth;

        // UI Unitialisation
        slider.maxValue = maxHealth;
        UpdateHealthUI(currentHealth);

        // Initialise Animator
        animator = GetComponent<Animator>();    
    }
    public override void TakeDamage(float damage)
    {
        Debug.Assert(damage >= 0, "Damage cannot be negative!");
        currentHealth -= damage;
        UpdateHealthUI(currentHealth);
        // Flinch Animation
        animator.Play("ElectricGhost_Hit_Idle");
       

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public override void Die()
    {
        animator.SetBool("isDead", true);
    }
}
