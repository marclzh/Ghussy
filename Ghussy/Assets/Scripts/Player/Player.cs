using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // TODO Interactable Script
    public InventoryObject inventory;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAnimator playerAnimator;
    public static bool IsPlayerTransformed = false;
    //[SerializeField] private PlayerController controller;
    // public PlayerAttack attack;
    [SerializeField] private BasePossessionState currentState;
    [SerializeField] PlayerWeapon currentWeapon;
    [SerializeField] Ability currentAbility;

    
    void OnHit(float damage) 
    {
        playerAnimator.PlayerHit();
        playerHealth.TakeDamage(damage);
    }

    private void Update()
    {
        Debug.Log(currentState);
    }

    public void SetState(BasePossessionState nextState)
    {
        if (nextState != null && !currentState.Same(nextState))
        {
            currentState = nextState;
            //currentWeapon = nextState.GetWeapon();
            currentAbility = nextState.GetAbility();
            Debug.Log(currentState);
        }
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
