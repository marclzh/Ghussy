using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kryz.CharacterStats;

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

    public Slider baseHealthSlider;
    public Gradient gradient;
    public Image baseHealthBar;

    private void Awake()
    {

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


    public void UpdateHealthUI(float health)
    {
        if (!isTransformed)
        {
            baseHealthSlider.value = health;
            baseHealthBar.color = gradient.Evaluate(1f);
            healthValue.text = $"{baseHealthSlider.value.ToString()}/{baseHealthSlider.maxValue.ToString()}";
        }
        else if (isTransformed)
        {
            transformationHealthSlider.value = health;
            transformationValue.text = $"{transformationHealthSlider.value.ToString()}/{transformationHealthSlider.maxValue.ToString()}";
        }
        else
        {
            Debug.Log("problem in update heatlh UI");
        }
    }

    public void UpdateMaxHealthUI(CharacterStat newMaxHealth)
    {
        baseHealthSlider.maxValue = newMaxHealth.Value;
        healthValue.text = $"{baseHealthSlider.value.ToString()}/{baseHealthSlider.maxValue.ToString()}";
    }

    public void UpdateCurrentHealthUI(CharacterStat newCurrentHealth)
    {
        baseHealthSlider.value = newCurrentHealth.Value;
        healthValue.text = $"{baseHealthSlider.value.ToString()}/{baseHealthSlider.maxValue.ToString()}";
    }

    public void Die()
    {
        Debug.Log("passway");
        animator.IsPlayerDead(true);
    }

    public void TransformationDeath()
    {
        animator.IsTransformationDead();
        // Disables the shield bar
        transformationHealthObject.SetActive(false);
        // This method changes the state of the player and the weapon 
        GetComponent<Player>().TransformationDeathUpdateState();
        isTransformed = false;
    }

    public void TransformationUpdateHealth(BasePossessionState nextState)
    {
        if (nextState != null)
        {
            isTransformed = true;
            //Debug.Log("transformhealthtest");
            transformationHealthObject.SetActive(true);
        }
    }

}
