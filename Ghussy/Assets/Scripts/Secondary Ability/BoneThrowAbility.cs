using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/Bone Throw Ability")]
public class BoneThrowAbility : Ability
{
    public override void Activate()
    {
        Debug.Log("Throw Bones");
    }
}
