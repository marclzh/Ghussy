using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ICharacter, IDamageable
{
    [SerializeField] private string enemyName;

    protected Health enemyHealth;

    public Health health => enemyHealth;

    string ICharacter.Name => enemyName;

    public void TakeDamage(float damageAmount)
    {
        enemyHealth.TakeDamage(damageAmount);
    }
}
