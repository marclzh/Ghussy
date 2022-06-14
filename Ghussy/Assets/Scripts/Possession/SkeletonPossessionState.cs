using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Possession State", menuName = "Possession State/Skeleton")]
public class SkeletonPossessionState : BasePossessionState
{
    [SerializeField] PlayerWeapon weapon;
    [SerializeField] PlayerAbility ability;
    public new string possessionStateName;
    [TextArea(15, 20)]
    public new string description;
}
