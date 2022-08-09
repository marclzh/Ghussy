using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InteractionPromptUI : MonoBehaviour
{
    // Player Controller References
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InputActionReference interactIA;

    // UI Elements
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject uiPanel;
    
    // Display Boolean
    public bool isDisplayed = false;

    private void Start()
    {
        uiPanel.SetActive(false);
    }
      
    public void SetUp(string text)
    {
        // Retrieve Key Binding from Input Action Reference and assign symbol to interaction display
        int bindingIndex = interactIA.action.GetBindingIndexForControl(interactIA.action.controls[0]);
        keyText.text = InputControlPath.ToHumanReadableString(
                interactIA.action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        // Assign Interaction Prompt text and open display
        promptText.text = text;
        uiPanel.SetActive(true);

        // Update boolean
        isDisplayed = true;
    }

    public void Close()
    {
        // Close display
        uiPanel.SetActive(false);
        
        // Update boolean
        isDisplayed = false;
    }

}
