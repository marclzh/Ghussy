using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaceOfPowerInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] Transform placeOfPower;

    // Interaction Fields
    [SerializeField] private string prompt;

    public string InteractionPrompt => prompt;
    public bool Interact(Interactor interactor)
    {
        placeOfPower.gameObject.SetActive(true);
        FindObjectOfType<PlayerController>().ActionMapMenuChange();
        AudioManager.Instance.Play("PlaceOfPower");

        return true;
    }

}


