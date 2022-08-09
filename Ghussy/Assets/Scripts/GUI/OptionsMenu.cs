using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private PlayerController playerController; 

public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void CloseOptions()
    {
        // Audio
        AudioManager.Instance.Play("Click");

        // Closes Options menu
        gameObject.SetActive(false);

        // Switch back player input map
        if (playerController != null) { playerController.ActionMapPlayerChange(); }
    }
}
