using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats;

public class PlayerStats 
{
    [SerializeField] private static CharacterStat movementSpeed = new CharacterStat(1f);
    [SerializeField] private static CharacterStat fireRate = new CharacterStat(1);
    [SerializeField] private static CharacterStat maxHealth = new CharacterStat(100);
    [SerializeField] private static CharacterStat damageModifier = new CharacterStat(1);
    
    public static void ModifyStat(CharacterStat stat, StatModifier statModifier)
    {
        stat.AddModifier(statModifier);        
    }
}
