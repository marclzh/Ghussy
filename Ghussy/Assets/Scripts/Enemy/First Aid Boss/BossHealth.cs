using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

/**
 * This class controls the logic of the boss' health.
 */
public class BossHealth : Health
{
    // Events to signify the boss' death and enragement.
    [SerializeField] VoidEvent OnBossDeath;
    [SerializeField] VoidEvent OnBossEnrage;

    // Reference to the boss' animator
    [SerializeField] EnemyAnimator bossAnimator;

    // Boss Health Values
    [SerializeField] float bossMaxHealth = 5000f;

    // Health UI Elements
    [SerializeField] private Slider baseHealthSlider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image baseHealthBar;
    [SerializeField] private TextMeshProUGUI healthValue;

    private void Start()
    {
        // Checks if item has been purchased
        if (SaveManager.instance.activeSave.shopBossHealthDeductionPurchased) { bossMaxHealth = 4000f; }

        // Initialise Health
        maxHealth = bossMaxHealth;
        currentHealth = maxHealth;

        // UI Unitialisation
        baseHealthSlider.maxValue = maxHealth;
        baseHealthBar.color = gradient.Evaluate(1f);
        UpdateHealthUI(maxHealth);

        // Initialise Animator
        bossAnimator = GetComponent<BossAnimator>();
        isInvincible = true;
    }
    private void Update()
    {
         DevKey_DamageBoss();
    }

    // Overriden TakeDamage method for the boss.
    public override void TakeDamage(float damage)
    {
        if (bossAnimator != null)
        {
            if (!isInvincible)
            {
                Debug.Assert(damage >= 0, "Damage cannot be negative!");
                currentHealth -= damage; // Reduce CurrentHealth Value
                UpdateHealthUI(currentHealth); // Update Health Bar

                // Flinch Animation
                bossAnimator.EnemyHit();

                if (currentHealth <= 2000)
                {
                    bossAnimator.BossEnraged();
                    OnBossEnrage.Raise();

                    AudioManager.Instance.Play("BossEnrage");
                }


                if (currentHealth <= 0)
                {
                    isInvincible = true;
                    // Enemy Death Animation
                    bossAnimator.EnemyDeath();

                    // Raises onEnemyDeath Event
                    StartCoroutine(bossDelayDeathEvent());

                    return;
                }
            }
        }   
    }

    // Method to update the health bar of the boss.
    public void UpdateHealthUI(float health)
    {
        baseHealthSlider.value = health;
        baseHealthBar.color = gradient.Evaluate(baseHealthSlider.normalizedValue);
        healthValue.text = $"{baseHealthSlider.value.ToString()}/{baseHealthSlider.maxValue.ToString()}";
    }

    private void DevKey_DamageBoss()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Boss Damaged");
            this.TakeDamage(4500);
        }
    }

    // Coroutine to delay the raising of the bossdeath enemy for the death animation to play.
    IEnumerator bossDelayDeathEvent()
    {
        yield return new WaitForSeconds(1f);
        OnBossDeath.Raise();
    }
}
