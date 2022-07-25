using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    private int playerBaseSceneIndex = 3;
    private int mainMenuSceneIndex = 0;
    private int tutorialSceneIndex = 2;
    private int abilityTutorialSceneIndex = 4;
    [SerializeField] float delay = 2;
    


    // Start is called before the first frame update
    void Start()
    {
        
        // Stop All Themes
        
    }

    public void DisplayDeathScreen()
    {
        StartCoroutine(DelaySetUp());

    }
 
    IEnumerator DelaySetUp()
    {
        yield return new WaitForSeconds(delay);

        Transform canvas = transform.GetChild(0);
        canvas.gameObject.SetActive(true);

        // Audio
        AudioManager.Instance.Stop("Theme");
        AudioManager.Instance.Stop("Boss1");

        System.Random rnd = new System.Random();
        int chance = rnd.Next(100);
        if (chance <= 5) { AudioManager.Instance.Play("GameOver1"); } else { AudioManager.Instance.Play("GameOver");  }
        
    }


    public void Continue()
    {
        AudioManager.Instance.Play("Click");

        // dies in tutorial
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadSceneAsync(tutorialSceneIndex);
        } 
        // dies in ability tutorial
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadSceneAsync(abilityTutorialSceneIndex);
        }
        else 
        {
            // Load Player Base
            SceneManager.LoadSceneAsync(playerBaseSceneIndex);
        }
        

        
    }

    public void Quit()
    {
        AudioManager.Instance.Play("Click");

        // Go back to main Menu
        SceneManager.LoadSceneAsync(mainMenuSceneIndex);
    }
}
