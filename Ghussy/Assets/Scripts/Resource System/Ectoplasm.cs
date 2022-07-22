using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ectoplasm : MonoBehaviour
{
    [SerializeField] public int amount;
    [SerializeField] public EctoplasmSource source;

    public void Awake()
    {
        if (source == EctoplasmSource.Enemy) { amount = Random.Range(0, 2); }
        if (source == EctoplasmSource.Boss) { amount = Random.Range(15, 25); }
        if (source == EctoplasmSource.Reward) { amount = 10; }
    }
}

public enum EctoplasmSource
{
    Enemy,
    Boss,
    Reward
}
