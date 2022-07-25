using UnityEngine;
using Kryz.CharacterStats;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, ICharacter, IDamageable
{

    [SerializeField] public InventoryObject ectoplasmInventory;
    [SerializeField] private InventoryObject memoryShardInventory;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerAnimator playerAnimator;
    public static bool IsPlayerTransformed = false;
    public bool hasDied;

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
    [SerializeField] public CharacterStatEvent currentHealthInitialization;
    [SerializeField] public CharacterStatEvent movementSpeedChange;
    [SerializeField] public CharacterStatEvent projectileSizeChange;



    public string Name => "Ghussy";

    public Health health => playerHealth;

    public void Start()
    {
        // TODO Move Save Data intialisation logic to Game Manager 
        saveManager = SaveManager.instance;
        SaveData currentSaveData = saveManager.activeSave;

        if (SaveManager.instance.hasLoaded)
        {
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

        // saveManager.activeSave.playerBaseGuide = true; // REMOVE THIS

        maxHealthInitialization.Raise(maxHealth);
        currentHealthInitialization.Raise(currentHealth);

        // Set Starting position
        float[] savedPosition = currentSaveData.playerPos;
        transform.position = savedPosition.Length == 0 ? new Vector3(0f, 0f, 0f) : new Vector3(savedPosition[0], savedPosition[1], savedPosition[2]);
        hasDied = false;
    }

    // Exposed save method for player to save the game
    public void ManualSave()
    {
        // Save Position and scene
        saveManager.activeSave.playerPos = new float[3] {transform.position.x, transform.position.y, transform.position.z};
        saveManager.activeSave.savePointSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Saves Game  
        saveManager.SaveGame();
    }

    public void SetState(BasePossessionState nextState)
    {
        if (nextState != null && !currentState.Same(nextState))
        {
            currentState = nextState;
            currentAbility = nextState.GetAbility();
        }
    }

    public void PlayerDeath()
    {
        if (!hasDied)
        {
            // Death Audio
            AudioManager.Instance.Play("Death");

            // Reset Saved Values

            // Reset Memory Shards
            saveManager.activeSave.memoryShardAmount = 0;
            memoryShardInventory.Container.Clear();
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
            saveManager.activeSave.permBoonApplied = false;

            // increasing death count 
            saveManager.activeSave.numOfDeaths++;

            // Prevents player from moving in death scene
            GetComponent<PlayerController>().ActionMapMenuChange();

            // Set boolean flag to true
            hasDied = true;

            saveManager.SaveGame();
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
        if (saveManager.hasLoaded) { saveManager.DeleteSaveData(); }

    }


    void OnHit(float damage)
    {
        // Play Hit Sound
        AudioManager.Instance.Play("PlayerHit");

        playerAnimator.PlayerHit();
        playerHealth.TakeDamage(damage);
    }

    // Checks Player Contact Enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnHit(10);
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            OnHit(25);
        }

        if (collision.gameObject.CompareTag("BossMelee"))
        {
            OnHit(150);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        playerHealth.TakeDamage(damageAmount);
    }

    public void EquipBoon(BoonItem boon)
    {
        boon.Equip(this);
    }

    public void UnequipBoon(BoonItem boon)
    {
        boon.Unequip(this);
    }

    public void ApplyPermBoons()
    {
        SaveData save = saveManager.activeSave;
        float movementSpeedBonus = .05f * save.permBoonMultiple[0];
        float maxHealthBonus = .05f * save.permBoonMultiple[1];
        float projectileSizeBonus = .05f * save.permBoonMultiple[2];

        movementSpeed.AddModifier(new StatModifier(movementSpeedBonus, StatModType.PercentMult, this));
        maxHealth.AddModifier(new StatModifier(maxHealthBonus, StatModType.PercentMult, this));
        currentHealth.AddModifier(new StatModifier(maxHealthBonus, StatModType.PercentMult, this));
        projectileSize.AddModifier(new StatModifier(projectileSizeBonus, StatModType.PercentMult, this));

        maxHealthInitialization.Raise(maxHealth);
        currentHealthInitialization.Raise(currentHealth);
        movementSpeedChange.Raise(movementSpeed);
        projectileSizeChange.Raise(projectileSize); 
    }

    public void purchaseBoon(int cost)
    {
        if (ectoplasmInventory.Container.Count <= 0) 
        {
            Debug.Log("Not Enough");
            return;  
        }

        if (cost <= ectoplasmInventory.Container[0].amount)
        {
            ectoplasmInventory.Container[0].amount -= cost;
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
