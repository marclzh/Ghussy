using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] VoidEvent OnEnemyDeath;

    public void OnDeath()
    {
        OnEnemyDeath.Raise();
       
    }
   
}
