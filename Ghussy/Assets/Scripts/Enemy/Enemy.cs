using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter, IDameagable
{
    [SerializeField] private string enemyName;

    private Health enemyHealth;

    public void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    public Health health => enemyHealth;

    string ICharacter.Name => enemyName;

    public void TakeDamage(float damageAmount)
    {
        enemyHealth.TakeDamage(damageAmount);
    }
}
