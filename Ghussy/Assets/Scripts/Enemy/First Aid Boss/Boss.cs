using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    private string bossName;

    [SerializeField] private Health enemyHealth;

    public void Start()
    {
        enemyHealth = GetComponent<BossHealth>();
    }

    public Health health => enemyHealth;

    public string Name => bossName;

    public void TakeDamage(float damageAmount)
    {
        enemyHealth.TakeDamage(damageAmount);
    }
}
