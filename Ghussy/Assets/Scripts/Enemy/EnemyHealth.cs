using TMPro;
using UnityEngine;
using UnityEngine.UI;

/**
 * Class to control the Enemy's Health.
 */
public class EnemyHealth : Health
{
    // Prefab References to the enemy's drops.
    [SerializeField] private GameObject ectoplasmPrefab;
    [SerializeField] private GameObject memoryShardPrefab;

    // Boolean to check if player has died.
    private bool hasDied = false;

    // Event to signify if enemy has died.
    [SerializeField] VoidEvent OnEnemyDeath;

    // Reference to the enemy's animator.
    [SerializeField] EnemyAnimator enemyAnimator;

    // Reference to the damage numbers appearing on the enemy.
    [SerializeField] private Transform damageTextPrefab;

    // References to the enemy's Health bar.
    public Slider baseHealthSlider;
    public Gradient gradient;
    public Image baseHealthBar;

    private void Start()
    {
        // Initialise Health
        maxHealth = 150;
        currentHealth = maxHealth;
        isInvincible = false;

        // UI Unitialisation
        baseHealthSlider.maxValue = maxHealth;
        //UpdateHealthUI(currentHealth);
        baseHealthBar.color = gradient.Evaluate(1f);

        // Initialise Animator
        enemyAnimator = GetComponent<EnemyAnimator>();

        UpdateHealthUI(currentHealth);
    }
    
    // Method to override the TakeDamage health method.
    public override void TakeDamage(float damage)
    {
        if (enemyAnimator != null)
        {
            if (!isInvincible)
            {
                Debug.Assert(damage >= 0, "Damage cannot be negative!");
                currentHealth -= damage;
                UpdateHealthUI(currentHealth);
                
                // Flinch Animation
                enemyAnimator.EnemyHit();
                enemyAnimator.EnemyStopAttack();

                // Spawn Damage Text
                if (damageTextPrefab != null)
                {
                    DisplayDamageNumbers(damage);
                }


                if (currentHealth <= 0 && hasDied == false)
                {
                    // setting enemy to invincible
                    isInvincible = true;
                    // Mark Enemy as dead and destroy Enemy
                    hasDied = true;
                   

                    // Raises onEnemyDeath Event
                    OnEnemyDeath.Raise();

                    // Spawn Ectoplasm and memory shards
                    ectoplasmPrefab.GetComponent<Ectoplasm>().source = EctoplasmSource.Enemy;
                    memoryShardPrefab.GetComponent<MemoryShard>().source = MemoryShardSource.Enemy;
                    Instantiate(ectoplasmPrefab, transform.position + new Vector3(0, Random.Range(0, .32f)), Quaternion.identity);
                    Instantiate(memoryShardPrefab, transform.position + new Vector3(0, Random.Range(0, .5f)), Quaternion.identity);

                    // Enemy Death Animation
                    enemyAnimator.EnemyDeath();
                    Destroy(gameObject, .5f); // Fix this magic number

                    return;
                }
            }
        }  
    }

    // Method to display the damage numbers appearing on the enemy.
    private void DisplayDamageNumbers(float damage)
    {
        var text = Instantiate(damageTextPrefab, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMeshPro>().text = "- " + damage.ToString();

    }

    // Method to update the Health UI of the enemy.
    public void UpdateHealthUI(float health)
    {
        baseHealthSlider.value = health;
        baseHealthBar.color = gradient.Evaluate(baseHealthSlider.normalizedValue);
    }
}
