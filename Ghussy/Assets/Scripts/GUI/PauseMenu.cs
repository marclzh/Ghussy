using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject SettingsMenu;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && !SettingsMenu.activeSelf)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        playerController.playerInput.SwitchCurrentActionMap("Menu");

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        playerController.playerInput.SwitchCurrentActionMap("Player");

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Options()
    {
        SettingsMenu.SetActive(true);
    }

    public void Quit()
    {
        SceneManager.LoadSceneAsync(0);
    }
}


