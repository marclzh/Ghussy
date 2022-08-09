using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/**
 * Class handling main menu scene logic
 */
public class MainMenu : MonoBehaviour
{
    // UI and helper fields
    [SerializeField] private int openingSceneIndex;
    [SerializeField] private Animator transition;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    // Start is called before the first frame update
    void Start()
    {
        // Displays load button only if save file exists
        bool saveDataExists = SaveManager.instance.hasLoaded;

        if (saveDataExists)
        {
            loadGameButton.gameObject.SetActive(true);
        }
    }

    // New Game Button
    public void NewGame()
    {
        // Audio
        AudioManager.Instance.Play("Click");
        AudioManager.Instance.Stop("Main Menu Theme");  // Stops main theme 

        // Delete Saves Data
        if (SaveManager.instance.hasLoaded) { SaveManager.instance.DeleteSaveData(); }

        // Loads Opening Scene
        SceneManager.LoadScene(openingSceneIndex);
    }

    // Load Game Button
    public void LoadGame()
    {
        // Audio
        AudioManager.Instance.Play("Click");
        AudioManager.Instance.Stop("Main Menu Theme");
     
        // Loads Last Scene
        int savePointIndex = SaveManager.instance.activeSave.savePointSceneIndex;
        SceneManager.LoadScene(savePointIndex);     
    }

    // Quit Game Button
    public void QuitGame()
    {
        // Audio
        AudioManager.Instance.Play("Click");

        Application.Quit();
    }
}
