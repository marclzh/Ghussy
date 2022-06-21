using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    // 1 frame delay for flinch animation to play
    [SerializeField] private float deathDelay = 0.5f;
    [SerializeField] private GameObject ectoplasmPrefab;

    //private bool isHurt = false;

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


           if (currentHealth <= 0)
           {
           // DestroyOnExit script only handles the destruction of
            // enemyGFX, so need to independently call Destroy on the 
            // whole enemy. Returns after to break out of method.
            Destroy(gameObject);
            enemyAnimator.EnemyDeath();
            Instantiate(ectoplasmPrefab, transform.position + new Vector3(0, Random.Range(0,.16f)), Quaternion.identity);
            return;
            }
        }
        
    }
}
