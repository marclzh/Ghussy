using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    // 1 frame delay for flinch animation to play
    [SerializeField] private float deathDelay = 0.3f;

    //private bool isHurt = false;

    EnemyAnimator enemyAnimator;


    private void Start()
    {
        // Initialise Health
        maxHealth = 100;
        currentHealth = maxHealth;

        // UI Unitialisation
        slider.maxValue = maxHealth;
        UpdateHealthUI(currentHealth);
        fill.color = gradient.Evaluate(1f);

        // Initialise Animator
        enemyAnimator = GetComponent<EnemyAnimator>();

    }

    public override void TakeDamage(float damage)
    {
        //isHurt = true;

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
            enemyAnimator.EnemyDeath();
            Destroy(gameObject, deathDelay);
            return;
        }
    }
}
