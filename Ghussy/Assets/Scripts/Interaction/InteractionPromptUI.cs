using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private GameObject uiPanel;
    public bool isDisplayed = false;

    private void Start()
    {
        uiPanel.SetActive(false);
    }
      
    private void LateUpdate()
    {
        
    }

    public void SetUp(string text)
    {
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
