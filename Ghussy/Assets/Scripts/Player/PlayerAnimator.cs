using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    
    //private bool isDead;
    //private bool isAttacking;
    //private bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /*
    // Update is called once per frame
    void Update()
    {
        
        animator.SetBool("isFiring", isAttacking);
        animator.SetBool("isMoving", isMoving);
    }
    */

    public void IsPlayerMoving(bool b)
    {
        animator.SetBool("isMoving", b); 
    }

    public void IsPlayerAttacking(bool b)
    {
        animator.SetBool("isFiring", b);
    }
    public void PlayerHit()
    {
        animator.Play("ghussy_hit");
    }
    public void BaseTransform()
    {
        animator.SetBool("isTransforming", true);
    }
    public void PlayerTransform(BasePossessionState basePossesssssessionState)
    {
        string nextState = basePossesssssessionState.toString();
        animator.SetBool(nextState, true);
    }
    public void IsPlayerDead(bool b)
    {
        animator.SetBool("isDead", b);
    }

}
