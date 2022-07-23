using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricGhost : Enemy
{
    public void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }


}
