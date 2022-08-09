using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

/**
 * Method to control the cutscene management of the scenes.
 */
public class CutsceneManager : MonoBehaviour
{
    // Reference to the current next cutscene to be played.
    [SerializeField] private PlayableDirector playableDirector;
    // Event to signify the start of a dialogue.
    [SerializeField] private VoidEvent OnDialogueStart;
    // List of cutscenes.
    [SerializeField] private List<PlayableDirector> cutscenes;
    // Reference to the baseGuide dialogues.
    [SerializeField] private GameObject baseGuideDialogueParent;

    public void Start()
    {
        if (playableDirector != null)
        {
            // This is for the control of the cutscenes in the base.
            if (playableDirector.name == "AghostineBaseGuide")
            {
                // Base Cutscene
                // 0 - baseguide cutscene
                // 1 - respawn cutscene
                if (SaveManager.instance.activeSave.playerBaseGuide == false)
                {
                    playableDirector.Play();
                } 
                else
                {
                    baseGuideDialogueParent.SetActive(false);
                    if (SaveManager.instance.activeSave.numOfDeaths > 0)
                    {
                        SwapCutscene(1);
                    }
                }
            }
        }
    }

    // Raises the dialogue start event.
    public void RaiseDialogueStart()
    {
        OnDialogueStart.Raise();
    }

    // Plays the currently active cutscene.
    public void Play()
    {
        playableDirector.Play();
    }

    // Swaps to the next cutscene in the list of cutscenes.
    public void SwapCutscene(int cutsceneNum)
    {
        playableDirector = cutscenes[cutsceneNum];
        playableDirector.Play();
    }

    // Disables the cutscene in the parameter field.
    public void disableCutscene(int cutsceneNum)
    {
        cutscenes[cutsceneNum].gameObject.SetActive(false);
    }

    // Changes the scene of the game.
    public void changeScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    // This makes the player base cutscene not play
    // upon subsequent entries of the player base.
    public void SetPlayerBaseSaveTrigger()
    {
        SaveManager.instance.activeSave.playerBaseGuide = true;
        SaveManager.instance.SaveGame();
    }
}
