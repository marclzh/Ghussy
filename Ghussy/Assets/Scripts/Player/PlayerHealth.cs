using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : Health
{
    TextMeshProUGUI healthValue;

    private void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;

        // UI Unitialisation
        slider.maxValue = maxHealth;
        healthValue = GameObject.Find("PlayerHealthBar/Health Value").GetComponent<TextMeshProUGUI>();
        UpdateHealthUI(currentHealth);
    }

    protected override void UpdateHealthUI(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
        healthValue.text = $"{slider.value.ToString()}/{slider.maxValue.ToString()}";
    }

    public override void Die()
    {
        PlayerAnimator animator = GetComponent<PlayerAnimator>();
        animator.IsPlayerDead(true);
    }

}
