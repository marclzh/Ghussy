using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used only for main menu
public class Placeholder : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    void OnEnable()
    {
        playerController.playerInput.SwitchCurrentActionMap("Menu");
    }
}
