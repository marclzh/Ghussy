using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    // 1 frame delay for flinch animation to play
    [SerializeField] private GameObject ectoplasmPrefab;
    [SerializeField] private GameObject memoryShardPrefab;

    private bool hasDied = false;

    [SerializeField] VoidEvent OnEnemyDeath;

    [SerializeField] EnemyAnimator enemyAnimator;

    public Slider baseHealthSlider;
    public Gradient gradient;
    public Image baseHealthBar;

    private void Start()
    {
        // Initialise Health
        maxHealth = 100;
        currentHealth = maxHealth;

        // UI Unitialisation
        baseHealthSlider.maxValue = maxHealth;
        //UpdateHealthUI(currentHealth);
        baseHealthBar.color = gradient.Evaluate(1f);

        // Initialise Animator
        enemyAnimator = GetComponent<EnemyAnimator>();

    }

    public override void TakeDamage(float damage)
    {
        if (enemyAnimator != null)
        {
          
           Debug.Assert(damage >= 0, "Damage cannot be negative!");
           currentHealth -= damage;
           UpdateHealthUI(currentHealth);
           // Flinch Animation
           enemyAnimator.EnemyHit();


           if (currentHealth <= 0 && hasDied == false)
           {
                // Mark Enemy as dead and destroy Enemy
                hasDied = true;
                Destroy(gameObject);

                // Enemy Death Animation
                enemyAnimator.EnemyDeath();

                // Raises onEnemyDeath Event
                OnEnemyDeath.Raise();

                // Spawn Ectoplasm and memory shards
                ectoplasmPrefab.GetComponent<Ectoplasm>().source = EctoplasmSource.Enemy;
                memoryShardPrefab.GetComponent<MemoryShard>().source = MemoryShardSource.Enemy;
                Instantiate(ectoplasmPrefab, transform.position + new Vector3(0, Random.Range(0,.32f)), Quaternion.identity);
                Instantiate(memoryShardPrefab, transform.position + new Vector3(0, Random.Range(0, .5f)), Quaternion.identity);

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
