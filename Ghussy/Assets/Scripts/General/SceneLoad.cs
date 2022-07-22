using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] Scene currentScene;

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += ChangeActiveScene;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= ChangeActiveScene;
    }

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public static void ChangeActiveScene(Scene first, Scene second)
    {
        
    }
}
