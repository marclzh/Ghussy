/**
 * This class contains behaviours of the boss enemy specific to itself.
 */
public class Boss : Enemy
{
    private BossHealth bossHealth;

    public void Start()
    {
        bossHealth = GetComponent<BossHealth>();
    }

    public void BossTrigger()
    {
        GetComponent<BossAnimator>().PlayerApproach();
        bossHealth.InvincibilityToggle(false);
    }
}
