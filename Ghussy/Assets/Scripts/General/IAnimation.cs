using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interface for all GameObjects which need to implement animation.
public interface IAnimation
{
    // state will be an enum which will be played with method animator.play() in 
    // inheriting subclasses.
    public void ChangeAnimationState(string newState);
}
