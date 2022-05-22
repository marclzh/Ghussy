using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimWeapon : MonoBehaviour
{
    // Player Component References
    public Transform player;
    public Transform firePoint; // Gameobject representing firepoint of weapon
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody;
    Animator animator;

    // Player Input Actions (CONSIDER REMOVING THIS OR REPURPOSING FOR OTHER MEANS)
    private EctoGearInputActions Controls;
    private InputAction fire;

    // Weapon Aiming/Firing Fields
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate;
    private float lastFireTime = 0.4f;
    private float offSetDistance; // Distance from player to weapon
    Vector2 mousePos;
    bool isFiring;

    // Weapon Threshold Fields to Prevent Sprite Flipping (TO BE IMPLEMENTED)
    public float mouseOffSet; // Threshold value to prevent sprite flipping 

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
        offSetDistance = transform.position.x - player.position.x;
    }

    private void Update()
    {
        CheckCurrentMousePos(); // Retrieves current mouse position
    }

    void CheckCurrentMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleAiming();
    }
    
    // Function allows the weapon to rotate according to current mouse position
    void HandleAiming()
    {
         // TO BE IMPLEMENTED : THRESHOLD CODE
         // if (Mathf.Abs(mousePos.x - player.position.x) >= mouseOffSet && Mathf.Abs(mousePos.y - player.position.y) >= mouseOffSet)
        
            // Rotates weapon
            Vector3 directionPlayerIsLooking = mousePos - rigidBody.position;
            float angle = Mathf.Atan2(directionPlayerIsLooking.y, directionPlayerIsLooking.x) * Mathf.Rad2Deg;
            rigidBody.rotation = angle;
            rigidBody.position = player.position + (offSetDistance * directionPlayerIsLooking.normalized);

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
    
    public void HandleShooting(bool weaponFired)
    {
        // Bullet Instantiation
        if (weaponFired && withinFireRate())
        {
            GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            Vector2 shootingDirection = new Vector2(mousePos.x, mousePos.y) - rb.position;
            rb.velocity += bulletSpeed * Time.deltaTime * shootingDirection;
           
            // Handle weapon shooting animation
            animator.SetBool("isFiring", true);

            // reset fire rate check
            lastFireTime = Time.time;

        } else {
            animator.SetBool("isFiring", false);
        }
    }

    private bool withinFireRate()
    {
        return (Time.time > (lastFireTime + fireRate));
    }
   
    private void StartFiring(InputAction.CallbackContext obj)
    {
        isFiring = true;
    }

    private void StopFiring(InputAction.CallbackContext obj)
    {
        isFiring = false;  
    }
}
