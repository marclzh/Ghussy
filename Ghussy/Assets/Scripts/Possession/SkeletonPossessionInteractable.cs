using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonPossessionInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] BasePossessionState possessionState;
    [SerializeField] PossessionEvent possessionEvent;
    [SerializeField] private string prompt;
    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        if (!Player.IsPlayerTransformed)
        {
            // Raise Event that object has been interacted with 
            possessionEvent.Raise(possessionState);
            Debug.Log("Event Raised!!");
            Destroy(gameObject);

        }
        return true;
    }

  
}
