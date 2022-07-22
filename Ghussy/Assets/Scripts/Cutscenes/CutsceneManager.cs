using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    // Children of this object is the
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private VoidEvent OnDialogueStart;
    [SerializeField] private List<PlayableDirector> cutscenes;
    [SerializeField] private GameObject baseGuideDialogueParent;

    public void Start()
    {
        if (playableDirector != null)
        {
            if (playableDirector.name == "AghostineBaseGuide")
            {
                if (SaveManager.instance.activeSave.playerBaseGuide == false)
                {
                    playableDirector.Play();
                } 
                else
                {
                    baseGuideDialogueParent.SetActive(false);
                }
            }
        }
    }
    public void RaiseDialogueStart()
    {
        OnDialogueStart.Raise();
    }
    public void Play()
    {
        playableDirector.Play();
    }
    public void swapCutscene(int cutsceneNum)
    {
        playableDirector = cutscenes[cutsceneNum];
        playableDirector.Play();
    }

    public void disableCutscene(int cutsceneNum)
    {
        cutscenes[cutsceneNum].gameObject.SetActive(false);
    }

    public void changeScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void SetPlayerBaseSaveTrigger()
    {
        SaveManager.instance.activeSave.playerBaseGuide = true;
        SaveManager.instance.SaveGame();
    }
}
