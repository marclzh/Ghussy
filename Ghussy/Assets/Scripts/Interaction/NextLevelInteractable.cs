using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] string nextLevel;
    [SerializeField] private string prompt;
    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private VectorValue playerStorage;
    public string InteractionPrompt => prompt;

    public bool Interact(Interactor interactor)
    {
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadSceneAsync(nextLevel);
        return true;
    }

  
}
