using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public string firstLevel;
    //[SerializeField] private GameObject mainMenuSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //mainMenuSprite.GetComponent<Animator>().SetBool("PlayButtonClicked", true);
        StartCoroutine(LoadGame());
    }

    public IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenOptions()
    {
        // NOT USED
    }

    public void CloseOptions()
    {
        // NOT USED
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
