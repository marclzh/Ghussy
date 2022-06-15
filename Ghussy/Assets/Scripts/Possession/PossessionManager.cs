using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossessionManager : MonoBehaviour
{
    private BasePossessionState currentState;
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PossessionEvent possessionEvent;

    public void Transform(BasePossessionState nextState)
    {
       // if (nextState != currentState)
        //{
            //playerAnimator.PlayerTransform(nextState);
            possessionEvent.Raise(nextState);
            
        //}

        Debug.Log("transformation works!!");
    }
}
