using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : Health
{
    protected const string ENEMY_DEATH = "Dead";
    protected const string ENEMY_FLINCH = "Flinch";
    protected const string ENEMY_IDLE = "Idle";

    // 1 frame delay for flinch animation to play
    [SerializeField] private float flinchDelay = 0.3f;
    [SerializeField] private float deathDelay = 0.3f;

    private bool isHurt = false;

    EnemyAnimation enemyAnimator;


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
        enemyAnimator = GetComponent<EnemyAnimation>();
        
    }

    public override void TakeDamage(float damage)
    {
        isHurt = true;

        Debug.Assert(damage >= 0, "Damage cannot be negative!");
        currentHealth -= damage;
        UpdateHealthUI(currentHealth);
        // Flinch Animation
        enemyAnimator.ChangeAnimationState(ENEMY_FLINCH);
       

        if (currentHealth <= 0)
        {
            // DestroyOnExit script only handles the destruction of
            // enemyGFX, so need to independently call Destroy on the 
            // whole enemy. Returns after to break out of method.
            enemyAnimator.ChangeAnimationState(ENEMY_DEATH);
            Destroy(gameObject, deathDelay);
            return;
        }

        Invoke("flinchComplete", flinchDelay);
    }

    // method to exit out of flinch frame
    private void flinchComplete()
    {
        isHurt = false;

        enemyAnimator.ChangeAnimationState(ENEMY_IDLE);
    }
}
