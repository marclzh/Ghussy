using UnityEngine;
using Kryz.CharacterStats;

public class Player : Character
{
    
    [SerializeField] private InventoryObject ectoplasmInventory;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAnimator playerAnimator;
    public static bool IsPlayerTransformed = false;
    
    public BasePossessionState currentState; 
    public BasePossessionState defaultState;
    [SerializeField] PlayerWeapon currentWeapon;
    [SerializeField] Ability currentAbility;
    [SerializeField] WeaponManager weaponManager;
    public VectorValue startingPosition;

    [SerializeField] public CharacterStat movementSpeed;
    [SerializeField] public CharacterStat maxHealth;
    [SerializeField] public CharacterStat maxTransformationHealth;

    public void OnValidate()
    {
        movementSpeed.BaseValue = GameData.movementSpeedValue;
        maxHealth.BaseValue = GameData.maxHealthValue;
        
        maxTransformationHealth.BaseValue = GameData.maxTransformationValue;
        
    }
    public void Start()
    {
        transform.position = startingPosition.initialValue;
    }

    void OnHit(float damage) 
    {
        playerAnimator.PlayerHit();
        playerHealth.TakeDamage(damage);
    }

    private void Update()
    {
       // Debug.Log(movementSpeed.Value);
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

    public void TransformationDeathUpdateState()
    {
        SetState(defaultState);
        weaponManager.GetComponent<WeaponManager>().SetState(currentState);
    }


    // Resets values
    private void OnApplicationQuit()
    {
        ectoplasmInventory.Container.Clear();
        // Fixed the value for testing 
        startingPosition.initialValue = new Vector2(-0.1f, 0.373f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnHit(10);
        }
    }

  
}
