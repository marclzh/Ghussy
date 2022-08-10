using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private CharacterStatEvent onStatChange;
    [SerializeField] private CharacterStatEvent onStatChange1;
    public void MovementSpeedPercentagePowerUp(float percentage)
    { 
        Player player = FindObjectOfType<Player>();
        CharacterStat ms = player.movementSpeed;
        ms.AddModifier(new StatModifier(percentage, StatModType.PercentMult, this));
        onStatChange.Raise(ms);
    }

    public void MaxHealthPercentagePowerUp(float percentage)
    {
        Player player = FindObjectOfType<Player>();
        CharacterStat maxHealth = player.maxHealth;
        CharacterStat currentHealth = player.currentHealth;


        float currMH = maxHealth.Value;
        // Increment max health
        maxHealth.AddModifier(new StatModifier(percentage, StatModType.PercentMult, this));
        float newMH = maxHealth.Value;
        float healthGained = newMH - currMH;
        // Increment current health by increase in max health
        currentHealth.AddModifier(new StatModifier(healthGained, StatModType.Flat, this));

        // Raise Event to updaet UI
        onStatChange.Raise(maxHealth);
        onStatChange1.Raise(currentHealth);
    }

    public void ProjectileSizePercentagePowerUp(float percentage)
    {
        Player player = FindObjectOfType<Player>();
        CharacterStat ProjectileSize = player.projectileSize;
        ProjectileSize.AddModifier(new StatModifier(percentage, StatModType.PercentMult, this));
        onStatChange.Raise(ProjectileSize);
    }

    public void CurrentHealthPercentagePowerUp(float percentage)
    {
        Player player = FindObjectOfType<Player>();
        CharacterStat currentHealth = player.currentHealth;
        currentHealth.AddModifier(new StatModifier(percentage, StatModType.PercentMult, this));
        onStatChange.Raise(currentHealth);
    }
}
