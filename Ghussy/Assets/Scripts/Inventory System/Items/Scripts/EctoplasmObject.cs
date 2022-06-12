using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ectoplasm Object", menuName = "Inventory System/Items/Ectoplasm")]
public class EctoplasmObject : ItemObject
{

    private int ectoplasmAmount = 1; 

    private void Awake()
    {
        type = ItemType.Ectoplasm;
    }

    public void SetAmount(int amount)
    {
        ectoplasmAmount = amount;
    }

    public int GetAmount()
    {
        return ectoplasmAmount;
    }
}


