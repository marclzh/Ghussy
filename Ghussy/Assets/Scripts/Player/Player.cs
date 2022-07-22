using UnityEngine;
using Kryz.CharacterStats;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, ICharacter, IDamageable
{

    [SerializeField] private InventoryObject ectoplasmInventory;
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
            //currentWeapon = nextState.GetWeapon();
            currentAbility = nextState.GetAbility();
            Debug.Log(currentState);
        }
    }

    public void PlayerDeath()
    {
        
        if (!hasDied)
        {
            AudioManager.Instance.Play("Death");

            // Reset Values
            saveManager.activeSave.memoryShardAmount = 0;
            saveManager.activeSave.savePointSceneIndex = 3; // Player Base
            saveManager.activeSave.numOfRoomsCompleted = 0;
            GetComponent<PlayerController>().ActionMapMenuChange();

            hasDied = true;
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
        saveManager.DeleteSaveData();

    }


    void OnHit(float damage)
    {
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

    // Helper Function TO BE REMOVED
    public void ClearInventory()
    {
        memoryShardInventory.Container.Clear();
        ectoplasmInventory.Container.Clear();
    }
}
