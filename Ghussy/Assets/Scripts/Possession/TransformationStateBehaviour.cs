using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This adds additional behaviour to the transformation state in the player's state machine.
 */
public class TransformationStateBehaviour : StateMachineBehaviour
{
    // This disables movement and toggles invincibility for the player.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerController>().ActionMapMenuChange();
        animator.gameObject.GetComponent<PlayerHealth>().InvincibilityToggle(true);
    }

    // This enables movement and toggles invincibility for the player.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerController>().ActionMapPlayerChange();
        animator.gameObject.GetComponent<PlayerHealth>().InvincibilityToggle(false);
    }
}
