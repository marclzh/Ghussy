using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
