using UnityEngine;

/**
 * This class resets the trigger for the player back to the default state.
 */
public class TransformationDeathReset : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("Default");
    }
}
