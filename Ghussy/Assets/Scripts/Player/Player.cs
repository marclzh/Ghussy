using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // TODO Interactable Script
    // TODO Inventory
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnHit(20);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {  
         if (collision.gameObject.tag == "Enemy")
         {
              OnHit(10);
         }
            
    }
       
    

}
