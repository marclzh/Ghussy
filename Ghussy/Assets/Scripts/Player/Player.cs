using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // TODO Interactable Script
    public InventoryObject inventory;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAnimator playerAnimator;
    //[SerializeField] private PlayerController controller;
    // public PlayerAttack attack;
    // public GameObject currentWeapon = null;
    // public PlayerAbility ability = null;

    
    void OnHit(float damage) 
    {
        playerAnimator.PlayerHit();
        playerHealth.TakeDamage(damage);
    }

    private void Update()
    {

    }

    // Resets values
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnHit(10);
        }
    }
}
