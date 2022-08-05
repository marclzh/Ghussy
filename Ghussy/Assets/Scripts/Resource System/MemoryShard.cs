using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryShard : MonoBehaviour
{
    [SerializeField] public int amount;
    [SerializeField] public MemoryShardSource source;

    public void Start()
    {
        if (source == MemoryShardSource.Enemy) { amount = Random.Range(1, 5); }
        if (source == MemoryShardSource.Boss) { amount = Random.Range(50, 100); }
        if (source == MemoryShardSource.Reward) { amount = 50; }
    }
}

public enum MemoryShardSource
{
    Enemy,
    Boss,
    Reward
}
