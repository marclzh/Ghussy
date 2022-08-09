using UnityEngine;

/**
 * This class controls the destruction of object related to the animator upon exit of the state.
 */
public class DestroyOnExit : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);   
    }
}
