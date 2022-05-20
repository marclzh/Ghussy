using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // public access modifiers allows us to edit these variables directly in Unity

    public float moveSpeed = 1f; // Movement speed of character
    public float collisionOffset = 0.025f; // Distance of collision detection
    public ContactFilter2D movementFilter; // determines where a collision can occur (layers)
    public Camera camera;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // List of collisions found during raycast 
    bool canMove = true; // Allows us to ensure that the player cannot move while attacking


    // Player Component References
    Vector2 movementInput;
    Vector2 mousePos;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = camera.ScreenToWorldPoint(Mouse.current.position.ReadValue()); // retrieve current mouse position
    }

    private void FixedUpdate()
    {
        // Movement Check
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

                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
            }
         

            // setting the direction of the sprite to the movement direction
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            Vector2 directionPlayerIsLooking = mousePos - rigidBody.position;
            float angle = Mathf.Atan2(directionPlayerIsLooking.y, directionPlayerIsLooking.x) * Mathf.Rad2Deg - 90f;
            rigidBody.rotation = angle;
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

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void OnFire()
    {

    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
