using UnityEngine;
using Kryz.CharacterStats;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, ICharacter, IDamageable
{
    // Player Inventories 
    [SerializeField] public InventoryObject ectoplasmInventory;
    [SerializeField] public InventoryObject memoryShardInventory;

    // Player Component References
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAnimator playerAnimator;

    // Exposed boolean fields
    public static bool IsPlayerTransformed = false;
    public bool hasDied;

    // Possession and Weapon references
    public BasePossessionState currentState;
    public BasePossessionState defaultState;
    [SerializeField] PlayerWeapon currentWeapon;
    [SerializeField] WeaponManager weaponManager;
    [SerializeField] Ability currentAbility;

    // Character Stats
    [SerializeField] public CharacterStat movementSpeed;
    [SerializeField] public CharacterStat maxHealth;
    [SerializeField] public CharacterStat maxTransformationHealth;
    [SerializeField] public CharacterStat currentTransformationHealth;
    [SerializeField] public CharacterStat currentHealth;
    [SerializeField] public CharacterStat projectileSize;

    [SerializeField] public CharacterStatEvent maxHealthInitialization;
    [SerializeField] public CharacterStatEvent currentHealthInitialization;
    [SerializeField] public CharacterStatEvent movementSpeedChange;
    [SerializeField] public CharacterStatEvent projectileSizeChange;

    // Character name reference
    public string Name => "Ghussy";

    // Damageable health reference
    public Health health => playerHealth;

    public void Start()
    {
        SaveData currentSaveData = SaveManager.instance.activeSave;

        if (SaveManager.instance.hasLoaded)
        {
            // Initialise Character Stats
            movementSpeed = new CharacterStat(currentSaveData.movementSpeedValue);
            maxHealth = new CharacterStat(currentSaveData.maxHealthValue);
            maxTransformationHealth = new CharacterStat(currentSaveData.maxTransformationValue);
            currentTransformationHealth = new CharacterStat(currentSaveData.currentTransformationValue);
            currentHealth = new CharacterStat(currentSaveData.currentHealthValue);
            projectileSize = new CharacterStat(currentSaveData.projectileSize);

            // Handle Perm Buffs 
            if (!currentSaveData.permBoonApplied) { ApplyPermBoons(); currentSaveData.permBoonApplied = true; }

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

        // Raises Event to initialise UI
        maxHealthInitialization.Raise(maxHealth);
        currentHealthInitialization.Raise(currentHealth);

        // Set Starting Position
        float[] savedPosition = currentSaveData.playerPos;
        transform.position = savedPosition.Length == 0 ? new Vector3(0f, 0f, 0f) : new Vector3(savedPosition[0], savedPosition[1], savedPosition[2]);
        
        // Initialise boolean to false
        hasDied = false;
    }

    // Exposed save method for player to save the game
    public void ManualSave()
    {
        // Save Position and scene
        SaveManager.instance.activeSave.playerPos = new float[3] {transform.position.x, transform.position.y, transform.position.z};
        SaveManager.instance.activeSave.savePointSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Saves Game  
        SaveManager.instance.SaveGame();
    }

    // Updates the player's current possession state and corresponding ability
    public void SetState(BasePossessionState nextState)
    {
        if (nextState != null && !currentState.Same(nextState))
        {
            currentState = nextState;
            currentAbility = nextState.GetAbility();
        }
    }

    // Reset values when player has died
    public void PlayerDeath()
    {
        if (!hasDied)
        {
            // Death Audio
            AudioManager.Instance.Play("Death");

            // Reset Saved Values
            SaveManager saveManager = SaveManager.instance;

            // Reset Saved Scene Index
            saveManager.activeSave.savePointSceneIndex = 3; // Player Base

            // Reset number of rooms completed and room indexes
            saveManager.activeSave.numOfRoomsCompleted = 0;
            saveManager.activeSave.roomCompleted_M = new bool[] { false, false, false };
            saveManager.activeSave.roomCompleted_E = new bool[] { false, false, false };
            saveManager.activeSave.roomCompleted_P = new bool[] { false, false, false };

            // Reset player Stats
            saveManager.activeSave.currentHealthValue = 100f;
            saveManager.activeSave.maxHealthValue = 100f;
            saveManager.activeSave.movementSpeedValue = 1f;
            saveManager.activeSave.currentHealthValue = 100f;
            saveManager.activeSave.projectileSize = 1f;
            saveManager.activeSave.permBoonApplied = false;

            // increasing death count 
            saveManager.activeSave.numOfDeaths++;

            // Prevents player from moving in death scene
            GetComponent<PlayerController>().ActionMapMenuChange();

            // Set boolean flag to true
            hasDied = true;

            // Set shop items to unpurchased
            saveManager.activeSave.shopBossHealthDeductionPurchased = false;
            saveManager.activeSave.shopBossSkeletonPurchased = false;
            saveManager.activeSave.shopEnemyNumberDeductionPurchased = false;

            // Saves game
            saveManager.SaveGame();
        }
    }

    // Reset player fields when exiting possession
    public void TransformationDeathUpdateState()
    {
        SetState(defaultState);
        weaponManager.GetComponent<WeaponManager>().SetState(currentState);
    }


    // Deletes save data when game is quit
    private void OnApplicationQuit()
    {
        if (SaveManager.instance.hasLoaded) { SaveManager.instance.DeleteSaveData(); }

    }

    // Checks Player Contact Enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Deals Different Damage according to enemy types
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            TakeDamage(25);
        }

    }

    public void TakeDamage(float damageAmount)
    {
        // Play Hit Sound
        AudioManager.Instance.Play("PlayerHit");

        // Animator
        playerAnimator.PlayerHit();

        // Health logic
        playerHealth.TakeDamage(damageAmount);
    }

    public void ApplyPermBoons()
    {
        // Retrieve save data
        SaveData save = SaveManager.instance.activeSave;
        float movementSpeedBonus = .05f * save.permBoonMultiple[0];
        float maxHealthBonus = .05f * save.permBoonMultiple[1];
        float projectileSizeBonus = .05f * save.permBoonMultiple[2];

        // Default Values
        movementSpeed = new CharacterStat(1f);
        maxHealth = new CharacterStat(100f);
        currentHealth = new CharacterStat(100f);
        projectileSize = new CharacterStat(1f);

        // Add Modifiers
        movementSpeed.AddModifier(new StatModifier(movementSpeedBonus, StatModType.PercentMult, this));
        maxHealth.AddModifier(new StatModifier(maxHealthBonus, StatModType.PercentMult, this));
        currentHealth.AddModifier(new StatModifier(maxHealthBonus, StatModType.PercentMult, this));
        projectileSize.AddModifier(new StatModifier(projectileSizeBonus, StatModType.PercentMult, this));

        // Update UI
        maxHealthInitialization.Raise(maxHealth);
        currentHealthInitialization.Raise(currentHealth);
        movementSpeedChange.Raise(movementSpeed);
        projectileSizeChange.Raise(projectileSize); 
    }

    // For purchases made in Shop or Place of Power
    public void purchaseBoon(int cost, ResourceType type)
    {
        if (type == ResourceType.Ectoplasm)
        {
            if (ectoplasmInventory.Container.Count <= 0)
            {
                Debug.Log("Not Enough");
                return;
            }

            if (cost <= ectoplasmInventory.Container[0].amount)
            {
                ectoplasmInventory.Container[0].amount -= cost;
                SaveManager.instance.activeSave.ectoplasmAmount -= cost;
            }
        
        }

        if (type == ResourceType.MemoryShard)
        {
            if (memoryShardInventory.Container.Count <= 0)
            {
                Debug.Log("Not Enough");
                return;
            }

            if (cost <= memoryShardInventory.Container[0].amount)
            {
                memoryShardInventory.Container[0].amount -= cost;
                SaveManager.instance.activeSave.memoryShardAmount -= cost;
            }

        }
    }

    // For Cutscene Use
    public void KillTransformation()
    {
        if (currentState != defaultState)
        {
            TakeDamage(9999);
        }
    }
}
