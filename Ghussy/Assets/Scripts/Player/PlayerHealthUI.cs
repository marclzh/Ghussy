using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kryz.CharacterStats;

/**
 * This class controls the Health UI of the player character.
 */
public class PlayerHealthUI : MonoBehaviour
{

    // Reference to the Player Object
    [SerializeField] private GameObject player;
    // Health Value display
    TextMeshProUGUI healthValue;
    // Shield Value display
    TextMeshProUGUI transformationValue;
    // Actual Shield Value
    private float transformationHealthValue;
    // Color of shield bar
    Color transformationColor;
    // References to the HealthBar image and overall gameobject
    [SerializeField] private Image transformationHealthBar;
    [SerializeField] private GameObject transformationHealthObject;
    // Slider for shield health
    [SerializeField] Slider transformationHealthSlider;
    // Boolean to keep track of transformation state
    private bool isTransformed = false;
    // Reference to the player animator
    PlayerAnimator animator;
    // Reference to the HealthBar slider.
    public Slider baseHealthSlider;
    // Reference to the gradient of the health bar.
    public Gradient gradient;
    // Reference to the overall health bar of the player.
    public Image baseHealthBar;

    private void Awake()
    {
        // Initialising variables
        transformationColor = new Color(0.1f, 0.95f, 0.95f);
        transformationHealthBar.color = transformationColor;
        animator = GetComponent<PlayerAnimator>();

        // UI Initialisation
        baseHealthSlider.maxValue = GetComponent<Player>().maxHealth.Value;
        transformationHealthValue = GetComponent<Player>().maxTransformationHealth.Value;
        healthValue = GameObject.Find("PlayerHealthBar/Health Value").GetComponent<TextMeshProUGUI>();
        transformationValue = GameObject.Find("TransformHealthBar/TransformationHealthValue").GetComponent<TextMeshProUGUI>();

        transformationHealthObject.SetActive(false);
    }

    // Method used by event system to update the health of the player.
    public void UpdateHealthUI(float health)
    {
        if (!isTransformed)
        {
            baseHealthSlider.value = health;
            baseHealthBar.color = gradient.Evaluate(baseHealthSlider.normalizedValue);
            healthValue.text = $"{ ((int)baseHealthSlider.value).ToString()}/{((int)baseHealthSlider.maxValue).ToString()}";
        }
        else if (isTransformed)
        {
            transformationHealthSlider.value = health;
            transformationValue.text = $"{((int)transformationHealthSlider.value).ToString()}/{((int)transformationHealthSlider.maxValue).ToString()}";
        }
        else
        {
            return;
        }
    }

    // Method to change MaxHealth when powerup is obtained.
    public void UpdateMaxHealthUI(CharacterStat newMaxHealth)
    {
        baseHealthSlider.maxValue = newMaxHealth.Value;
        baseHealthBar.color = gradient.Evaluate(1f);
        healthValue.text = $"{ ((int)baseHealthSlider.value).ToString()}/{ ((int)baseHealthSlider.maxValue).ToString()}";
    }

    // Method to update the current health of the player.
    public void UpdateCurrentHealthUI(CharacterStat newCurrentHealth)
    {
        baseHealthSlider.value = newCurrentHealth.Value;
        healthValue.text = $"{ ((int)baseHealthSlider.value).ToString()}/{ ((int)baseHealthSlider.maxValue).ToString()}";
    }

    // Method to play the death animation of the player.
    public void Die()
    {
        animator.IsPlayerDead(true);
    }

    // Method to update the UI upon the death of the transformation.
    public void TransformationDeath()
    {
        animator.IsTransformationDead();
        // Disables the shield bar
        transformationHealthObject.SetActive(false);
        // This method changes the state of the player and the weapon 
        GetComponent<Player>().TransformationDeathUpdateState();
        isTransformed = false;
    }

    // Method to update the health of the transformation.
    public void TransformationUpdateHealth(BasePossessionState nextState)
    {
        if (nextState != null)
        {
            isTransformed = true;
            transformationHealthObject.SetActive(true);
        }
    }

}
