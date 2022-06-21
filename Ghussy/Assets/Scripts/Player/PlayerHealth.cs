using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : Health
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
    private bool isTransformed;
    // Reference to the player animator
    PlayerAnimator animator;



    private void Start()
    {
        // Initialising the values of the variables
        maxHealth = 100;
        transformationHealthValue = 100;
        currentHealth = maxHealth;
        transformationColor = new Color(0.1f, 0.95f, 0.95f);
        transformationHealthBar.color = transformationColor;
        animator = GetComponent<PlayerAnimator>();

        // UI Initialisation
        baseHealthSlider.maxValue = maxHealth;
        healthValue = GameObject.Find("PlayerHealthBar/Health Value").GetComponent<TextMeshProUGUI>();
        transformationValue = GameObject.Find("TransformHealthBar/TransformationHealthValue").GetComponent<TextMeshProUGUI>();
        UpdateHealthUI(currentHealth);
        transformationHealthObject.SetActive(false);
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
        } 
        else
        {
            transformationHealthValue -= damage;
            UpdateHealthUI(damage);
            if (transformationHealthValue <= 0)
            {
                TransformationDeath();
            }
        }

    }

    protected override void UpdateHealthUI(float health)
    {
        if (!isTransformed)
        {
            baseHealthSlider.value = health;
            baseHealthBar.color = gradient.Evaluate(1f);
            healthValue.text = $"{baseHealthSlider.value.ToString()}/{baseHealthSlider.maxValue.ToString()}";
        } 
        else if (isTransformed)
        {
            transformationHealthSlider.value = transformationHealthValue;
            transformationValue.text = $"{transformationHealthSlider.value.ToString()}/{transformationHealthSlider.maxValue.ToString()}";
        }
        else
        {
            Debug.Log("problem in update heatlh UI");
        }
    }

    public override void Die()
    {
        animator.IsPlayerDead(true);
    }

    public void TransformationDeath()
    {
        animator.IsTransformationDead();
        // Disables the shield bar
        transformationHealthObject.SetActive(false);
        // This method changes the state of the player and the weapon 
        player.GetComponent<Player>().TransformationDeathUpdateState();
        isTransformed = false;
    }

    public void TransformationUpdateHealth(BasePossessionState nextState) 
    {
        if (nextState != null)
        {
            isTransformed = true;
            Debug.Log("transformhealthtest");
            transformationHealthObject.SetActive(true);
        }
    }

}
