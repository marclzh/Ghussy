using UnityEngine;
using UnityEngine.UI;

public class TutorialPromptUI : MonoBehaviour
{
    [SerializeField] Transform TriggerManager;
    [SerializeField] GameObject[] tutorialTriggers;
    [SerializeField] Sprite[] tutorialImages;
    [SerializeField] GameObject tutorialCanvas;
    [SerializeField] private Image currImage;
    private int currImageCounter;
    private int noOfTriggers;


    // player reference
    [SerializeField] private Transform player;

    public void Awake()
    {
        noOfTriggers = TriggerManager.childCount;
        currImageCounter = 0;
        currImage.sprite = tutorialImages[0];
    }

    public void DisableTrigger(string triggerName)
    {
        for (int i = 0; i < noOfTriggers - 1; i++)
        {
            if (tutorialTriggers[i].name == triggerName)
            {
                tutorialTriggers[i].SetActive(false);
            }
        }
    }

    public void IncreaseImage()
    {
        currImageCounter++;
        currImage.sprite = tutorialImages[currImageCounter];
    }

    public void DisableImage() 
    {
        tutorialCanvas.gameObject.SetActive(false);
    }

    public void EnableImage()
    {
        tutorialCanvas.gameObject.SetActive(true);
    }
}
