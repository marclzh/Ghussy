using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationStateBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerController>().ActionMapMenuChange();
        animator.gameObject.GetComponent<PlayerHealth>().InvincibilityToggle(true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerController>().ActionMapPlayerChange();
        animator.gameObject.GetComponent<PlayerHealth>().InvincibilityToggle(false);
    }
}
