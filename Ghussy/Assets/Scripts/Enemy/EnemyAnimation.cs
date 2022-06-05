using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour, IAnimation
{
    // Reference to the enemy's animator.
    public Animator animator;
    
    // Current animation state of the enemy.
    protected string currState;

    // Enums of the possible enemy animation states.
    protected const string ENEMY_IDLE = "Idle";
    protected const string ENEMY_MOVE = "Move";
    protected const string ENEMY_ATTACK = "Attack";
    protected const string ENEMY_FLINCH = "Flinch";
    protected const string ENEMY_DEATH = "Dead";
    void Start()
    {
        ChangeAnimationState(ENEMY_IDLE);
    }

    // Implemented changeAnimationTo method from Animation interface.
    public void ChangeAnimationState(string newState)
    {
        // do nothing if the curr state is the same as the new state.
        if (currState == newState)
        {
            return;
        }

        // plays the animation and sets the currState to the new state.
        animator.Play(newState);

        currState = newState;
    }
}
