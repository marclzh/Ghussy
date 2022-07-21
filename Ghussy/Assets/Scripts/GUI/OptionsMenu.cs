using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] PlayerController playerController; 

public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    
    public void SetFullscreen(bool isFullscreen) { 
        Screen.fullScreen = isFullscreen;
    } 

    public void CloseOptions()
    {
        gameObject.SetActive(false);
        // Switch back player input map
        if (playerController != null) { playerController.playerInput.SwitchCurrentActionMap("Player"); }
    }
}
