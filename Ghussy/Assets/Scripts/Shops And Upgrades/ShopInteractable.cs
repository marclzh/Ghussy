using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Transform shop;

    // Interaction Fields
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        shop.gameObject.SetActive(true);
        
        AudioManager.Instance.Play("PlaceOfPower");

        return true;
    }
}
