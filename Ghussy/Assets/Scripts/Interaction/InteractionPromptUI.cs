using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private InputActionReference interactIA;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject uiPanel;
    public bool isDisplayed = false;

    private void Start()
    {
        uiPanel.SetActive(false);
       
    }
      
    public void SetUp(string text)
    {
        int bindingIndex = interactIA.action.GetBindingIndexForControl(interactIA.action.controls[0]);
        keyText.text = InputControlPath.ToHumanReadableString(
                interactIA.action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        promptText.text = text;
        uiPanel.SetActive(true);
        isDisplayed = true;
    }

    public void Close()
    {
        uiPanel.SetActive(false);
        isDisplayed = false;
    }

}
