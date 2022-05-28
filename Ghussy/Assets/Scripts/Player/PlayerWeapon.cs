using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    // Player Component References
    public Transform weapon; // EctoGear
    public Transform firePoint; // Gameobject representing firepoint of weapon
    SpriteRenderer weaponSR;
    Rigidbody2D weaponRB;
    Animator weaponAnimator;

    // Player Input Actions 
    private PlayerInputActions controls;
    private InputAction fire;

    // Weapon Aiming/Firing Fields
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireRate;
    private float lastFireTime = 0.4f;
    private float offSetDistance; // Distance from player to weapon
    Vector2 mousePos;
    //bool isFiring;

    private void OnEnable()
    {
        // Enable Fire Action Map
        fire = controls.Player.Fire;
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
        controls = new PlayerInputActions();
    }


    // Start is called before the first frame update
    void Start()
    {
        weaponRB = weapon.GetComponent<Rigidbody2D>();
        weaponSR = weapon.GetComponent<SpriteRenderer>();
        weaponAnimator = weapon.GetComponent<Animator>();
        offSetDistance = weapon.position.x - transform.position.x;
    }

    private void Update()
    {
        CheckCurrentMousePos(); // Retrieves current mouse position
    }

    void CheckCurrentMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    
    void FixedUpdate()
    {
        HandleAiming();
    }

    // Function allows the weapon to rotate according to current mouse position
    void HandleAiming()
    {
        Vector2 playerPos2D = new Vector2(transform.position.x, transform.position.y);

        // Rotates weapon
        Vector3 directionPlayerIsLooking = mousePos - playerPos2D;
        float angle = Mathf.Atan2(directionPlayerIsLooking.y, directionPlayerIsLooking.x) * Mathf.Rad2Deg;
        weaponRB.rotation = angle;
        weaponRB.position = transform.position + (offSetDistance * directionPlayerIsLooking.normalized);

        // Flip Sprite 
        if (directionPlayerIsLooking.x < 0)
        {
            weaponSR.flipY = true;
        }
        else if (directionPlayerIsLooking.x > 0)
        {
            weaponSR.flipY = false;
        }

    }

    public void HandleShooting(bool weaponFired)
    {
        Vector2 playerPos2D = new Vector2(transform.position.x, transform.position.y);

        // Bullet Instantiation
        if (weaponFired && withinFireRate())
        {
            GameObject projectile = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            Vector2 shootingDirection = mousePos - playerPos2D;
            rb.velocity += bulletSpeed * Time.deltaTime * shootingDirection.normalized;

            // Handle weapon shooting animation
            weaponAnimator.SetBool("isFiring", true);

            // reset fire rate check
            lastFireTime = Time.time;

        }
        else
        {
            weaponAnimator.SetBool("isFiring", false);
        }
    }

    private bool withinFireRate()
    {
        return (Time.time > (lastFireTime + fireRate));
    }

    private void StartFiring(InputAction.CallbackContext obj)
    {
        //isFiring = true;
    }

    private void StopFiring(InputAction.CallbackContext obj)
    {
       // isFiring = false;
    }
}
