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
    public bool canPause;
    public GameObject pauseMenuUI;
    public GameObject SettingsMenu;

    public void Start()
    {
        canPause = false;
        StartCoroutine(delayPause()); 
    }
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


