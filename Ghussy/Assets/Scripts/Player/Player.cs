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
    [SerializeField] public CharacterStat currentTransformationHealth;
    [SerializeField] public CharacterStat currentHealth;

    [SerializeField] public CharacterStatEvent maxHealthInitialization;
    [SerializeField] public CharacterStatEvent currentHealthInitilization;

    public void Start()
    {
        // TODO Move Save Data intialisation logic to Game Manager 
        SaveData currentSaveData = SaveManager.instance.activeSave;

        if (SaveManager.instance.hasLoaded)
        {
            movementSpeed.BaseValue = currentSaveData.movementSpeedValue;
            maxHealth.BaseValue = currentSaveData.maxHealthValue;
            maxTransformationHealth.BaseValue = currentSaveData.maxTransformationValue;
            currentTransformationHealth.BaseValue = currentSaveData.currentTransformationValue;
            currentHealth.BaseValue = currentSaveData.currentHealthValue;
        }
        else
        {
            // Default Values
            movementSpeed = new CharacterStat(1f);
            maxHealth = new CharacterStat(100f);
            maxTransformationHealth = new CharacterStat(100f);
            currentTransformationHealth = new CharacterStat(100f);
            currentHealth = new CharacterStat(100f);

            // Saves Default Values;
            currentSaveData.movementSpeedValue = movementSpeed.Value;
            currentSaveData.maxHealthValue = maxHealth.Value;
            currentSaveData.maxTransformationValue = maxTransformationHealth.Value;
            currentSaveData.currentHealthValue = currentHealth.Value; 
            currentSaveData.currentTransformationValue = currentTransformationHealth.Value; 
            

        }

        maxHealthInitialization.Raise(maxHealth);
        currentHealthInitilization.Raise(currentHealth);

        // Set Starting position
        transform.position = startingPosition.initialValue;

    }


    void OnHit(float damage)
    {
        playerAnimator.PlayerHit();
        playerHealth.TakeDamage(damage);
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
