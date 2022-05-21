using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimWeapon : MonoBehaviour
{
    public Transform player;
    public float offSetDistance;
    public float mouseOffSet;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody;
    Vector2 mousePos;
    Animator animator;
    bool isFiring;

    // Player Input Actions
    private EctoGearInputActions Controls;
    private InputAction fire;

    private void OnEnable()
    {
        // Enable Fire Action Map
        fire = Controls.EctoGear.Fire;
        fire.started += StartFiring; // Executes when fire button is pressed
        fire.canceled += StopFiring; // Executes when fire button is released
        fire.Enable();
    }

    private void OnDisable()
    {
        fire.Disable();
    }


    private void Awake()
    {
        Controls = new EctoGearInputActions();
    }


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        CheckCurrentMousePos();
    }

    void CheckCurrentMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        HandleAiming();

        if (isFiring)
        {
            animator.SetBool("isFiring", true);
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }

    void HandleAiming()
    {
      
        // THINK ABOUT THRESHOLD CODE
         // if (Mathf.Abs(mousePos.x - player.position.x) >= mouseOffSet && Mathf.Abs(mousePos.y - player.position.y) >= mouseOffSet)
        

            // Rotates weapon
            Vector3 directionPlayerIsLooking = mousePos - rigidBody.position;
            float angle = Mathf.Atan2(directionPlayerIsLooking.y, directionPlayerIsLooking.x) * Mathf.Rad2Deg;
            rigidBody.rotation = angle;
            rigidBody.position = player.position + (offSetDistance * directionPlayerIsLooking.normalized);

            //transform.localPosition = (offSetDistance * directionPlayerIsLooking.normalized);


            // Flip Sprite 
            if (directionPlayerIsLooking.x < 0)
            {
                spriteRenderer.flipY = true;
            }
            else if (directionPlayerIsLooking.x > 0)
            {
                spriteRenderer.flipY = false;
            }

    }

    void HandleShooting()
    {
        Debug.Log("Current" + isFiring);

        if (isFiring)
        {
            animator.SetBool("isFiring", true);
        }
        else
        {
            animator.SetBool("isFiring", false);
        }

    }
   

    private void StartFiring(InputAction.CallbackContext obj)
    {
        isFiring = true;
        Debug.Log("EG fire");
    }

    private void StopFiring(InputAction.CallbackContext obj)
    {
        isFiring = false;  
        Debug.Log("EG stop fire");
    }
}
