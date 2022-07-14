using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kryz.CharacterStats;

public class PlayerHealth : Health
{
    [SerializeField] private VoidEvent onTransformationDeath;
    [SerializeField] private FloatEvent onHealthChange;
    [SerializeField] private VoidEvent onPlayerDeath;

    // Reference to the Player Object
    [SerializeField] private GameObject player;

    // Actual Shield Value
    private float currentTransformationHealth;
    private float maxTransformationHealth;

    // Boolean to keep track of transformation state
    private bool isTransformed;

    private void Start()
    {
        // Initialising the values of the variables
        maxHealth = GetComponent<Player>().maxHealth.Value;
        maxTransformationHealth = GetComponent<Player>().maxTransformationHealth.Value;
        currentTransformationHealth = GetComponent<Player>().currentTransformationHealth.Value;
        currentTransformationHealth = 100f;
        currentHealth = GetComponent<Player>().currentHealth.Value;
        
        // Raises Event to update UI
        onHealthChange.Raise(currentHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            TakeDamage(99);
        }
    }

    public override void TakeDamage(float damage)
    {
        if (!isTransformed)
        {
            base.TakeDamage(damage);
            
            // Raise Event to Update UI
            onHealthChange.Raise(currentHealth);

            // Saves Changes to Current Health
            SaveManager.instance.activeSave.currentHealthValue = currentHealth;
        }
        else
        {
            currentTransformationHealth -= damage;

            // Raise Event to Update UI
            onHealthChange.Raise(currentTransformationHealth);

            // Saves Changes to Current Transformation Health
            SaveManager.instance.activeSave.currentTransformationValue = currentTransformationHealth;

            if (currentTransformationHealth <= 0)
            {
                
                isTransformed = false;
                onTransformationDeath.Raise();
            }

        }

    }

    public void Heal(float healAmount)
    {
        if (!isTransformed)
        {
            // Clamps value to not exceed max health
            currentHealth = Mathf.Clamp(currentHealth + healAmount, 0, maxHealth);

            // Raise Event to Update UI
            onHealthChange.Raise(currentHealth);

            // Saves Changes to Current Health
            SaveManager.instance.activeSave.currentHealthValue = currentHealth;
        }
        else
        {
            currentTransformationHealth = Mathf.Clamp(currentTransformationHealth + healAmount, 0, maxTransformationHealth);

            // Raise Event to Update UI
            onHealthChange.Raise(currentTransformationHealth);

            // Saves Changes to Current Transformation Health
            SaveManager.instance.activeSave.currentTransformationValue = currentTransformationHealth;


        }

    }

    public override void Die()
    {
        onPlayerDeath.Raise();
    }


    public void TransformationUpdateHealth(BasePossessionState nextState)
    {
        if (nextState != null)
        {
            isTransformed = true;
        }
    }

    public void MaxHealthPowerUp(CharacterStat newMaxHealth)
    {
        maxHealth = newMaxHealth.Value;

        
        // Saves Data
        SaveData currentSaveData = SaveManager.instance.activeSave;
        currentSaveData.maxHealthValue = newMaxHealth.Value;

    }

    public void CurrentHealthChange(CharacterStat health)
    {
        currentHealth = health.Value;

        // Saves Data
        SaveData currentSaveData = SaveManager.instance.activeSave;
        currentSaveData.currentHealthValue = health.Value;
    }

}
