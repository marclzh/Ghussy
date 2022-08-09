using UnityEngine;

/**
 *  This class centrally controls the animator of the Player Character.
 */
public class PlayerAnimator : MonoBehaviour
{
    // Private Animator Reference
    [SerializeField] private Animator animator;

    // Reference to the player's current possession state.
    private string currState;

    void Start()
    {
        animator = GetComponent<Animator>();
        currState = "Default";
    }

    // Sets the boolean to play the moving animation of the Player.
    public void IsPlayerMoving(bool b)
    {
        animator.SetBool("isMoving", b); 
    }

    // Sets the boolean to play the attacking animation of the Player.
    public void IsPlayerAttacking(bool b)
    {
        animator.SetBool("isFiring", b);
    }

    // Plays the flinching animation of the Player based on the current possession.
    public void PlayerHit()
    {
        if (currState == "Default")
        {
            animator.Play("ghussy_hit");
        } 
        else if (currState == "SkeletonTransformation")
        {
            animator.Play("skelly_hit");
        } 
        else if (currState == "FridgeTransformation")
        {
            animator.Play("fridge_hit");
        }
    }

    // Sets the boolean to play the transforming animation of the player.
    public void PlayerTransform()
    {
        animator.SetTrigger(currState);
    }

    // Plays the death animation of the player.
    public void IsPlayerDead(bool b)
    {
        animator.SetBool("isDead", b);
    }

    // Sets the trigger for the death of the player while transformed.
    public void IsTransformationDead()
    {
        animator.SetTrigger("isTransformDead");
        currState = "Default";  
    }

    // Used for the event to update the referenced possession state.
    public void UpdatePossessionState(BasePossessionState nextState)
    {
        currState = nextState.ToString();
        PlayerTransform();
    }
}
