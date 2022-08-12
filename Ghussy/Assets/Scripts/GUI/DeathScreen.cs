using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class handling death screen logic which appears after the player dies
 */
public class DeathScreen : MonoBehaviour
{
    // Respective Scene Indexes
    private int playerBaseSceneIndex = 3;
    private int mainMenuSceneIndex = 0;
    private int tutorialSceneIndex = 2;
    private int abilityTutorialSceneIndex = 4;

    // Delay time before death screen appears
    [SerializeField] float delay = 2f;
    
    // Exposed Method to set up death screen
    public void DisplayDeathScreen()
    {
        StartCoroutine(DelaySetUp());
    }
 
    IEnumerator DelaySetUp()
    {
        // Delay
        yield return new WaitForSeconds(delay);

        // Set display active
        Transform canvas = transform.GetChild(0);
        canvas.gameObject.SetActive(true);

        // Stop all current Audio
        AudioManager.Instance.Stop("Theme");
        AudioManager.Instance.Stop("Boss1");

        // Play random game over audio 
        System.Random rnd = new System.Random();
        int chance = rnd.Next(100);
        if (chance <= 5) { AudioManager.Instance.Play("GameOver1"); } else { AudioManager.Instance.Play("GameOver");  }
        
    }

    // Continue button on death screen, allows player to resume gameplay depending on point of death
    public void Continue()
    {
        // Audio
        AudioManager.Instance.Play("Click");

        // Player Dies in Main Tutorial
        if (SceneManager.GetActiveScene().buildIndex == tutorialSceneIndex)
        {
            // Reloads Scene
            SceneManager.LoadSceneAsync(tutorialSceneIndex);
        } 

        // Player dies in Ability Tutorial
        else if (SceneManager.GetActiveScene().buildIndex == abilityTutorialSceneIndex)
        {
            // Reloads Scene
            SceneManager.LoadSceneAsync(abilityTutorialSceneIndex);
        }
        else 
        {
            // Player dies in main game
            // Load Player Base
            SceneManager.LoadSceneAsync(playerBaseSceneIndex);
        }
    }

    // Quit Button
    public void Quit()
    {
        // Audio
        AudioManager.Instance.Play("Click");

        // Go back to main Menu
        SceneManager.LoadSceneAsync(mainMenuSceneIndex);
    }
}
