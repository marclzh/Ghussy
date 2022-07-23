using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kryz.CharacterStats;

public enum BoonType
{
    Permanent,
    Temporary
} 

[CreateAssetMenu]
public class BoonItem : ItemNew
{
    public float MovementSpeedPercentageBonus;
    public float MaxHealthPercentageBonus;
    public float ProjectileSizePercentageBonus;
    
    public void Equip(Player player)
    {
        if (MovementSpeedPercentageBonus != 0) { player.movementSpeed.AddModifier(new StatModifier(MovementSpeedPercentageBonus, StatModType.PercentMult, this)); }
        if (MaxHealthPercentageBonus != 0) { player.maxHealth.AddModifier(new StatModifier(MaxHealthPercentageBonus, StatModType.PercentMult, this)); }
        if (ProjectileSizePercentageBonus != 0) { player.projectileSize.AddModifier(new StatModifier(ProjectileSizePercentageBonus, StatModType.PercentMult, this)); }
    }

    public void Unequip(Player player)
    {
        player.movementSpeed.RemoveAllModifiersFromSource(this);
        player.maxHealth.RemoveAllModifiersFromSource(this);
        player.projectileSize.RemoveAllModifiersFromSource(this);

    }
}
