using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : EnemyAnimator
{
    public void isEnraged()
    {
        animator.SetBool("IsEnraged", true);
    }

    public void PlayerApproach()
    {
        animator.SetTrigger("PlayerApproach");
    }
    public override void EnemyHit()
    {
        return;
    }
}
