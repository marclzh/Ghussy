using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Memory Shard Object", menuName = "Inventory System/Items/Memory Shard")]
public class MemoryShardObject : ItemObject
{

    private void Awake()
    {
        type = ItemType.MemoryShard;
    }
}


