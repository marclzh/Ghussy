using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerScript : MonoBehaviour
{
    [SerializeField] TutorialPromptUI promptUI;
    [SerializeField] GameObject tutorialCanvas;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (tutorialCanvas.activeSelf == false)
            {
                tutorialCanvas.SetActive(true);
            }
            promptUI.IncreaseImage();
            gameObject.SetActive(false);
        }
    }
}
