using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Kryz.CharacterStats;

public class PlayerController : MonoBehaviour
{
    // Player Component References
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;

    // Player Input Actions
    public PlayerInputActions playerControls;
    public PlayerInput playerInput;
    private InputAction movePlayerIA;
    private InputAction fireWeaponIA;
    private InputAction useAbilityIA;
    private InputAction interactIA;
    
    // Movement Fields
    public float moveSpeed = 1f; // Movement speed of character
    public float collisionOffset = 0.025f; // Distance of collision detection
    public ContactFilter2D movementFilter; // determines where a collision can occur (layers)
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // List of collisions found during raycast 
    bool canMove = true; // Allows us to ensure that the player cannot move while attacking

    // Weapon Firing Fields
    bool isFiring; 
    [SerializeField] private PlayerWeapon weapon;

    // Animation Fields
    [SerializeField] private PlayerAnimator playerAnimator;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }
    private void OnEnable()
    {
        // Enable Move Input Action
        movePlayerIA = playerControls.Player.Move;
        movePlayerIA.Enable();

        // Enable Fire Input Action
        fireWeaponIA = playerControls.Player.Fire;
        fireWeaponIA.Enable();

        // Enable Interact Input Action
        interactIA = playerControls.Player.Interact;
        interactIA.Enable();

        // Enable Ability Input Action
        useAbilityIA = playerControls.Player.Ability;
        useAbilityIA.Enable();

    }

    private void OnDisable()
    {
        movePlayerIA.Disable();
        fireWeaponIA.Disable();
        interactIA.Disable();
        useAbilityIA.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
       Movement();
       Fire();
    }

    // Checks if player is currently firing, if true, call weapon's shoot method.
    // This method also handles animator logic for the player
    private void Fire()
    {
       if (isFiring)
       {
          weapon.HandleShooting(true);
          playerAnimator.IsPlayerAttacking(true);
        }       
        else
        {
          weapon.HandleShooting(false);
          playerAnimator.IsPlayerAttacking(false);
         }        
    }
    
    // Specific Namespace for New Input System - Do not change method name
    // Uses Hold and Release Interaction for button value type
    private void OnFire(InputValue value)
    {
        isFiring = value.isPressed; 
    }


    // Function that handles player movement
    // If player is able to move, perform collision check, and move player accordingly.
    // This function also handles player animation.
    private void Movement()
    {
        // Retrieve Movement input 
        Vector2 movementInput = movePlayerIA.ReadValue<Vector2>();

        // Check if player can move (ie. movement locked)
        if (canMove)
        {
            // If movement input is not 0, try to move
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                playerAnimator.IsPlayerMoving(true);
                
            }
            else
            {
                playerAnimator.IsPlayerMoving(false);
                //animator.SetBool("isMoving", false);
            }

            // setting the direction of the sprite to the movement direction
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;

                // when player is firing, if facing left, and weapon is right of player, flip player.
                if (isFiring)
                {
                    if (weapon.GetComponent<PlayerWeapon>().weapon.transform.position.x > transform.position.x)
                    {
                        spriteRenderer.flipX = false;
                    }
                }
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;

                // same as above, just opposite
                if (isFiring)
                {
                    if (weapon.GetComponent<PlayerWeapon>().weapon.transform.position.x < transform.position.x)
                    {
                        spriteRenderer.flipX = true;
                    }
                }
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero) // null check
        {
            // Check for potential collisions
            int count = rigidBody.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            // If no collision, move in direction and return true, else return false
            if (count == 0)
            {
                rigidBody.MovePosition(rigidBody.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // Can't move if there's no direction to move in
            return false;
        }
    }

    // Locks player movement
    public void LockMovement()
    {
        canMove = false;
    }

    // Unlocks player movement.
    public void UnlockMovement()
    {
        canMove = true;
    }

    // Movement Speed Power Up
    public void UpdateMovementSpeed(CharacterStat movementStat)
    {
        moveSpeed = movementStat.Value;
    }
    public void ActionMapMenuChange()
    {
        playerInput.SwitchCurrentActionMap("Menu");
        Debug.Log("Swapped To Menu ActionMap");
    }
    public void ActionMapPlayerChange()
    {
        playerInput.SwitchCurrentActionMap("Player");
        Debug.Log("Swapped To Player ActionMap");

    }
}
