using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private CharacterStatEvent onStatChange;
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
        maxHealth.AddModifier(new StatModifier(percentage, StatModType.PercentMult, this));
        onStatChange.Raise(maxHealth);
    }
}
