using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTriggerScript : MonoBehaviour
{
    [SerializeField] TutorialPromptUI promptUI;
    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            promptUI.IncreaseImage();
            gameObject.SetActive(false);
        }
    }
}
