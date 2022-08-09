/**
 * This class controls the animation states of the boss.
 */
public class BossAnimator : EnemyAnimator
{
    // Enables the boolean to trigger the boss' enraged state.
    public void isEnraged()
    {
        animator.SetBool("IsEnraged", true);
    }

    // Sets the trigger to be played when the player approaches the boss.
    public void PlayerApproach()
    {
        animator.SetTrigger("PlayerApproach");
    }

    // Overriden flinch method since boss should not flinch.
    public override void EnemyHit()
    {
        return;
    }
}
