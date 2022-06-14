using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public string firstLevel;
    public Animator transition;
    public float transitionTime = 2f;

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
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
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
