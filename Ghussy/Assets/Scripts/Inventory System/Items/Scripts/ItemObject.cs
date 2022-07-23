using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Healing, 
    Equipment,
    Default,
    Ectoplasm,
    MemoryShard

}


public class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
   
    public string itemName;

}
