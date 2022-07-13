using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound 
{
    public enum AudioTypes { soundEffect, music }
    public AudioTypes audioType;

    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;
    [Range (0f,1f)] 
    public float pitch;

    public bool loop;
    public bool playOnAwake;

    [HideInInspector]
    public AudioSource source;

}
