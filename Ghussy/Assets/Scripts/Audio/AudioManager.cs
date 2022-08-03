using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;

    [SerializeField] private Sound[] sounds;

    public static AudioManager Instance;
    public static bool hasInitialised;

    private string bgmKey = "MusicVolume";
    private string sfxKey = "SoundEffectsVolume";

    private void Awake()
    {
        //Singleton method
        if (Instance == null)
        {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (Instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Initialise Sounds
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            switch (s.audioType)
            {
                case Sound.AudioTypes.soundEffect:
                    s.source.outputAudioMixerGroup = soundEffectsMixerGroup;
                    break;

                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

            if (s.playOnAwake)
                s.source.Play();
        }

        hasInitialised = true;

    }

    private void Start()
    {
        UpdateMixerVolume();
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " Not Found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.name == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " does NOT exist!");
            return;
        }
        s.source.Stop();
    }

    public void UpdateMixerVolume()
    {
        // Load saved volume values
        float musicVolume = PlayerPrefs.GetFloat(bgmKey, 0.5f);
        float soundEffectsVolume = PlayerPrefs.GetFloat(sfxKey, 0.5f);

        musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(musicVolume) * 20);
        soundEffectsMixerGroup.audioMixer.SetFloat("Sound Effects Volume", Mathf.Log10(soundEffectsVolume) * 20);
    }
}
