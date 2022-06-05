using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ectoplasm Object", menuName = "Inventory System/Items/Ectoplasm")]
public class EctoplasmObject : ItemObject
{
    private void Awake()
    {
        type = ItemType.Ectoplasm;
    }
}
