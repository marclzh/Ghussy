using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePossessionState : ScriptableObject
{
    [SerializeField] PlayerWeapon weapon;
    [SerializeField] PlayerAbility ability;
    public string possessionStateName;
    [TextArea(15, 20)]
    public string description;

    public string toString()
    {
        return possessionStateName;
    }
}




