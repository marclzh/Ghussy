using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    // 1 frame delay for flinch animation to play
    [SerializeField] private int maxEctoplasmDrop;
    [SerializeField] private GameObject ectoplasmPrefab;

    private bool hasDied = false;

    [SerializeField] EnemyAnimator enemyAnimator;


    private void Start()
    {
        // Initialise Health
        maxHealth = 100;
        currentHealth = maxHealth;

        // UI Unitialisation
        baseHealthSlider.maxValue = maxHealth;
        UpdateHealthUI(currentHealth);
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
                gameObject.GetComponent<Enemy>().OnDeath();

                // Spawn Ectoplasm
                int num = Random.Range(1, maxEctoplasmDrop);
                while (num > 0)
                {
                    Instantiate(ectoplasmPrefab, transform.position + new Vector3(0, Random.Range(0,.32f)), Quaternion.identity);
                    num--;
                }
                
                return;
            }
        }
        
    }
}
