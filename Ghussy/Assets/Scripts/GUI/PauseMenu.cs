using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Class handling pause system of the game
 */
public class PauseMenu : MonoBehaviour
{
    // UI and Player references
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject SettingsMenu;

    // Boolean flags
    public bool GameIsPaused = false;
    private bool canPause;

    public void Start()
    {
        canPause = false;
        StartCoroutine(delayPause()); 
    }

    // Prevent player from pausing for the first few seconds of scene transition
    IEnumerator delayPause()
    {
        yield return new WaitForSeconds(1.5f);
        canPause = true;
    } 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && !SettingsMenu.activeSelf)
            {
                Resume();
            }
            else if (canPause && !GameIsPaused)
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        // Prevents player from moving
        playerController.playerInput.SwitchCurrentActionMap("Menu");

        // Open Display and Pause Game
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        // Reallow player movement
        playerController.playerInput.SwitchCurrentActionMap("Player");

        // Close Display and Unpause Game
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Options Menu 
    public void Options()
    {
        SettingsMenu.SetActive(true);
    }

    // Quit Button - Returns to Main Menu Scene
    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void DisablePausing()
    {
        canPause = false;
    }
    public void EnablePausing()
    {
        canPause = true;
    }
}


