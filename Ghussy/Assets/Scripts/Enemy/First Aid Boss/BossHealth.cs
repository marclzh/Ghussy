using UnityEngine;
using UnityEngine.UI;

public class BossHealth : Health
{
    // Boss Fields
    [SerializeField] VoidEvent OnEnemyDeath;
    [SerializeField] EnemyAnimator bossAnimator;

    // Boss Health Values
    [SerializeField] float bossMaxHealth = 1000f;
    [SerializeField] float bossCurrentHealth;


    // Health UI Elements
    [SerializeField] private Slider baseHealthSlider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image baseHealthBar;

    private void Start()
    {
        // Initialise Health
        maxHealth = bossMaxHealth;
        currentHealth = maxHealth;

        // UI Unitialisation
        baseHealthSlider.maxValue = maxHealth;
        baseHealthBar.color = gradient.Evaluate(1f);

        // Initialise Animator
        bossAnimator = GetComponent<EnemyAnimator>();
    }

    public override void TakeDamage(float damage)
    {
        if (bossAnimator != null)
        {
           Debug.Assert(damage >= 0, "Damage cannot be negative!");
           currentHealth -= damage; // Reduce CurrentHealth Value
           UpdateHealthUI(currentHealth); // Update Health Bar
            
           // Flinch Animation
           bossAnimator.EnemyHit();
            
           if (currentHealth <= 500)
           {
                bossAnimator.BossEnraged();
           }


           if (currentHealth <= 0)
           {
                // Mark Enemy as dead and destroy Enemy
               
                Destroy(gameObject);

                // Enemy Death Animation
                bossAnimator.EnemyDeath();

                // Raises onEnemyDeath Event
                OnEnemyDeath.Raise();
                
                return;
            }
        }
        
    }

    public void UpdateHealthUI(float health)
    {
            baseHealthSlider.value = health;
            baseHealthBar.color = gradient.Evaluate(baseHealthSlider.normalizedValue);
    }
}
