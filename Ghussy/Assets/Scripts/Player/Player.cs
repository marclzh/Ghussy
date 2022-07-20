using UnityEngine;
using Kryz.CharacterStats;

public class Player : MonoBehaviour, ICharacter, IDamageable
{

    [SerializeField] private InventoryObject ectoplasmInventory;
    [SerializeField] private InventoryObject memoryShardInventory;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAnimator playerAnimator;
    public static bool IsPlayerTransformed = false;

    public BasePossessionState currentState;
    public BasePossessionState defaultState;
    [SerializeField] PlayerWeapon currentWeapon;
    [SerializeField] Ability currentAbility;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] SaveManager saveManager;
  

    [SerializeField] public CharacterStat movementSpeed;
    [SerializeField] public CharacterStat maxHealth;
    [SerializeField] public CharacterStat maxTransformationHealth;
    [SerializeField] public CharacterStat currentTransformationHealth;
    [SerializeField] public CharacterStat currentHealth;
    [SerializeField] public CharacterStat projectileSize;

    [SerializeField] public CharacterStatEvent maxHealthInitialization;
    [SerializeField] public CharacterStatEvent currentHealthInitilization;

    public string Name => "Ghussy";

    public Health health => playerHealth;

    public void Start()
    {
        // TODO Move Save Data intialisation logic to Game Manager 
        saveManager = SaveManager.instance;
        SaveData currentSaveData = saveManager.activeSave;

        if (SaveManager.instance.hasLoaded)
        {
            movementSpeed.BaseValue = currentSaveData.movementSpeedValue;
            maxHealth.BaseValue = currentSaveData.maxHealthValue;
            maxTransformationHealth.BaseValue = currentSaveData.maxTransformationValue;
            currentTransformationHealth.BaseValue = currentSaveData.currentTransformationValue;
            currentHealth.BaseValue = currentSaveData.currentHealthValue;
            projectileSize.BaseValue = currentSaveData.projectileSize;

            // Resources
            if (ectoplasmInventory.Container.Count > 0) { ectoplasmInventory.Container[0].amount = currentSaveData.ectoplasmAmount; }
            if (memoryShardInventory.Container.Count > 0) { memoryShardInventory.Container[0].amount = currentSaveData.memoryShardAmount; }

        }
        else
        {
            // Default Values
            movementSpeed = new CharacterStat(1f);
            maxHealth = new CharacterStat(100f);
            maxTransformationHealth = new CharacterStat(100f);
            currentTransformationHealth = new CharacterStat(100f);
            currentHealth = new CharacterStat(100f);
            projectileSize = new CharacterStat(1f);

            // Saves Default Values;
            currentSaveData.movementSpeedValue = movementSpeed.Value;
            currentSaveData.maxHealthValue = maxHealth.Value;
            currentSaveData.maxTransformationValue = maxTransformationHealth.Value;
            currentSaveData.currentHealthValue = currentHealth.Value; 
            currentSaveData.currentTransformationValue = currentTransformationHealth.Value;
            currentSaveData.projectileSize = projectileSize.Value;

            // Resources
            ectoplasmInventory.Container.Clear();
            memoryShardInventory.Container.Clear();
        }

        maxHealthInitialization.Raise(maxHealth);
        currentHealthInitilization.Raise(currentHealth);

        // Set Starting position
        float[] savedPosition = currentSaveData.playerPos;
        transform.position = savedPosition.Length == 0 ? new Vector3(0f, 0f, 0f) : new Vector3(savedPosition[0], savedPosition[1], savedPosition[2]) ;
    }

    // Exposed save method for player to save the game
    public void ManualSave()
    {
        // Save Position
        saveManager.activeSave.playerPos = new float[3] {transform.position.x, transform.position.y, transform.position.z};

        // Saves Game  
        saveManager.SaveGame();
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

    public void PlayerDeath()
    {
        saveManager.activeSave.memoryShardAmount = 0;
    }

    public void TransformationDeathUpdateState()
    {
        SetState(defaultState);
        weaponManager.GetComponent<WeaponManager>().SetState(currentState);
    }


    // Resets values
    private void OnApplicationQuit()
    {


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnHit(10);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        playerHealth.TakeDamage(damageAmount);
    }

    // Helper Function TO BE REMOVED
    public void ClearInventory()
    {
        memoryShardInventory.Container.Clear();
        ectoplasmInventory.Container.Clear();
    }
}
