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
    public void RaiseDialogueStart()
    {
        OnDialogueStart.Raise();
    }
    public void Play()
    {
        playableDirector.Play();
    }
    public void SwapCutscene(int cutsceneNum)
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
