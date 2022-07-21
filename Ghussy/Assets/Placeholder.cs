using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used only for main menu
public class Placeholder : MonoBehaviour
{
    public PlayerController playerController;

    // Start is called before the first frame update
    void OnEnable()
    {
        playerController.playerInput.SwitchCurrentActionMap("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
