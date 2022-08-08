using UnityEngine;
using UnityEngine.InputSystem;
using Kryz.CharacterStats;

public class PlayerWeapon : MonoBehaviour
{
    // Player Component References
    public GameObject weapon;
    [SerializeField] private Transform playerReference;
    SpriteRenderer weaponSR;
    Rigidbody2D weaponRB;
    Animator weaponAnimator;
    [SerializeField] Player player;
    bool isTransformed = false;
    bool isAbilityActive = false;
    float projectileSize;

    // Player Input Actions 
    private PlayerInputActions controls;
    private InputAction fire;

    // Weapon Aiming/Firing Fields
    private float lastFireTime = 0.4f;
    private float offSetDistance; // Distance from player to weapon
    Vector2 mousePos;


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
        offSetDistance = Vector2.Distance(weapon.transform.position, playerReference.position);
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
        Vector2 playerPos2D = new Vector2(playerReference.position.x, playerReference.position.y);

        // Rotates weapon
        Vector3 directionPlayerIsLooking = mousePos - playerPos2D;
        float angle = Mathf.Atan2(directionPlayerIsLooking.y, directionPlayerIsLooking.x) * Mathf.Rad2Deg;
        weaponRB.rotation = angle;
        weaponRB.position = playerReference.position + (offSetDistance * directionPlayerIsLooking.normalized);

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
        Vector2 playerPos2D = new Vector2(playerReference.position.x, playerReference.position.y);

        // Bullet Instantiation
        if (weaponFired && withinFireRate())
        {
            // Apply projectile size bonus
            projectileSize = player.projectileSize.Value;
            weapon.GetComponent<Weapon>().bulletPrefab.transform.localScale = new Vector3 (projectileSize, projectileSize, 1);

            if (!isTransformed && !isAbilityActive)
            {
                // Handle Audio
                WeaponAudioCue();

                // Reset the fireRate to the base value (for laser)
                weapon.GetComponent<Weapon>().fireRate = weapon.GetComponent<Weapon>().GetBaseFireRate();
                

                GameObject projectile = Instantiate(weapon.GetComponent<Weapon>().bulletPrefab,
                    weapon.GetComponent<Weapon>().firePoint.position, weapon.GetComponent<Weapon>().firePoint.rotation);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                Vector2 shootingDirection = mousePos - playerPos2D;
                rb.velocity += weapon.GetComponent<Weapon>().bulletPrefab.GetComponent<BulletController>().speed * Time.deltaTime * shootingDirection.normalized;
            }
            else if (GetComponent<WeaponManager>().currentState.ToString() == "SkeletonTransformation")
            {
                Vector2 shootingDirection = mousePos - playerPos2D;
                weapon.GetComponent<BoneWeapon>().BoneShotgun(shootingDirection);                
            } 
            else if (GetComponent<WeaponManager>().currentState.ToString() == "FridgeTransformation")
            {
                
                weapon.GetComponent<IceWeapon>().EnableLaser();
                weapon.GetComponent<IceWeapon>().ShootLaser();
                
            }

            // Handle weapon shooting animation
            weaponAnimator.SetBool("isFiring", true);

            // reset fire rate check
            lastFireTime = Time.time;
        }
        else
        {
            if (GetComponent<WeaponManager>().currentState.ToString() == "FridgeTransformation")
            {
                weapon.GetComponent<IceWeapon>().DisableLaser();
                
            }
            
            weaponAnimator.SetBool("isFiring", false);
        }
    }

    private void WeaponAudioCue()
    {
        
        if (GetComponent<WeaponManager>().currentState.ToString() == "SkeletonTransformation")
        {
            // Bone Weapon
            AudioManager.Instance.Play("BoneWeapon");
        }
        else if (GetComponent<WeaponManager>().currentState.ToString() == "FridgeTransformation")
        {
            // Ice Weapon
            AudioManager.Instance.Play("IceWeapon");
        }
        else
        {
            // Default
            AudioManager.Instance.Play("WeaponFire1");
        }
    }

    public void updateWeapon(GameObject weapon)
    {
        this.weapon = weapon;
        weaponRB = weapon.GetComponent<Rigidbody2D>();
        weaponSR = weapon.GetComponent<SpriteRenderer>();
        weaponAnimator = weapon.GetComponent<Animator>();
        offSetDistance = Vector2.Distance(weapon.transform.position, playerReference.position);
    }

    private bool withinFireRate()
    {
        return (Time.time > (lastFireTime + weapon.GetComponent<Weapon>().fireRate));
    }

    private void StartFiring(InputAction.CallbackContext obj)
    {
        //isFiring = true;
    }

    private void StopFiring(InputAction.CallbackContext obj)
    {
        // isFiring = false;
    }

    public void IsTransformed()
    {
        isTransformed = true;
    }

    public void AbilityActivate()
    {
        isAbilityActive = true;
    }

    public void AbilityDeactivate()
    {
        isAbilityActive = false;
    }

    public void ProjectilSizeBonus(CharacterStat bonus)
    {
        projectileSize = bonus.Value;
        SaveManager.instance.activeSave.projectileSize = projectileSize;
    }
}