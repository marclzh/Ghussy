using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Possession State", menuName = "Possession State/New Possession State")]
public class BasePossessionState : ScriptableObject
{
    [SerializeField] PlayerWeapon weapon;
    [SerializeField] Ability ability;
    [SerializeField] private string possessionStateName;
    [TextArea(15, 20)]
    public string description;

    public override string ToString()
    {
        return possessionStateName;
    }

    public bool Same(BasePossessionState other)
    {
        return other.possessionStateName == this.possessionStateName;
      
    }
    
    public PlayerWeapon GetWeapon()
    {
        return weapon;
    }

    public Ability GetAbility()
    {
        return ability;
    }

}




