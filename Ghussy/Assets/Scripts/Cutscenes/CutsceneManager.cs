using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneManager : MonoBehaviour
{
    // Children of this object is the
    [SerializeField] private PlayableDirector playableDirector;
    [SerializeField] private VoidEvent OnDialogueStart;
    [SerializeField] private List<PlayableDirector> cutscenes;

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
}
