using UnityEngine;
using Kryz.CharacterStats;

/**
 * This class controls the logic of the Player's Health
 */
public class PlayerHealth : Health
{
    // Events to be raised to update the relevant information.
    [SerializeField] private VoidEvent onTransformationDeath;
    [SerializeField] private FloatEvent onHealthChange;
    [SerializeField] private VoidEvent onPlayerDeath;

    // Reference to the Player Object
    [SerializeField] private GameObject player;
    private bool hasDied;

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
        currentTransformationHealth = 100f; //  Default value
        currentHealth = GetComponent<Player>().currentHealth.Value;

        // Raises Event to update UI
        onHealthChange.Raise(currentHealth);
    }

    // Overidden TakeDamage Function to control the logic of taking damage for the player.
    public override void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            if (!isTransformed)
            {
                base.TakeDamage(damage);

                // Audio to be played on Hit
                FindObjectOfType<AudioManager>().Play("Hit");

                // Raise Event to Update UI
                onHealthChange.Raise(currentHealth);

                // Decrement health value
                GetComponent<Player>().currentHealth.AddModifier(new StatModifier(-damage, StatModType.Flat, this));

                // Saves Changes to Current Health
                SaveManager.instance.activeSave.currentHealthValue = currentHealth;
            }
            else
            {
                // Audio to be played on Hit
                FindObjectOfType<AudioManager>().Play("Hit");

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
    }

    // Healing Method to heal the player.
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

    // Overidden Death method from the parent Health class.
    public override void Die()
    {
        if (!hasDied)
        {
            onPlayerDeath.Raise();
            hasDied = true;
        }
        
    }
    
    // Method to update the Health of the transformation.
    public void TransformationUpdateHealth(BasePossessionState nextState)
    {
        if (nextState != null)
        {
            isTransformed = true;
        }
    }

    // Method to be called when the player collects a MaxHealth upgrade.
    public void MaxHealthPowerUp(CharacterStat newMaxHealth)
    {
        maxHealth = newMaxHealth.Value;

        // Saves Data
        SaveData currentSaveData = SaveManager.instance.activeSave;
        currentSaveData.maxHealthValue = newMaxHealth.Value;

    }

    // Method to change the current health of the player.
    public void CurrentHealthChange(CharacterStat health)
    {
        currentHealth = health.Value;

        // Saves Data
        SaveData currentSaveData = SaveManager.instance.activeSave;
        currentSaveData.currentHealthValue = health.Value;
    }
}