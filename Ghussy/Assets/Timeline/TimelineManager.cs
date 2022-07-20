using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    private PlayableDirector director;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void ResumeTimeline()
    {
        director.time = director.time;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void PauseTimeline()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
}
