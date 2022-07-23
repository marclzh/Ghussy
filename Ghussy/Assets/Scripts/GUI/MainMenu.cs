using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    [SerializeField] private int openingSceneIndex;
    [SerializeField] private Animator transition;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;

    // Start is called before the first frame update
    void Start()
    {
        bool saveDataExists = SaveManager.instance.hasLoaded;
        if (saveDataExists)
        {
            loadGameButton.gameObject.SetActive(true);
        }
        
    }

    public void StartButton()
    {
        // Game Loading logic will be called by animator to allow animation to complete
       
    }

    public void NewGame()
    {
        // Delete Saves Data
        if (SaveManager.instance.hasLoaded) { SaveManager.instance.DeleteSaveData(); }
        // Audio Manager reset
        AudioManager.Instance.Stop("Main Menu Theme");
        // Scene Transiiton
        //transition.SetTrigger("Start");
        // Loads Opening Scene
        SceneManager.LoadScene(openingSceneIndex);
    }

    public void LoadGame()
    {
      
        // Audio Manager reset
        AudioManager.Instance.Stop("Main Menu Theme");
        // Scene Transiiton
        //transition.SetTrigger("Start");
        // Loads Last Scene
        int savePointIndex = SaveManager.instance.activeSave.savePointSceneIndex;
        SceneManager.LoadScene(savePointIndex);     
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
