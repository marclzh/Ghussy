using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    bool volumeMuted;

    // Start is called before the first frame update
    void Start()
    {

        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
        PlayerPrefs.SetInt("volumeMuted", volumeMuted ? 1 : 0);
    }

    private void PressMuteButton()
    {
        if (volumeMuted == false)
        {
            volumeSlider.value = 0;
            ChangeVolume();
            volumeMuted = true;
            AudioListener.pause = true;
        }
        else
        {
            Load();
            volumeMuted = false;
            AudioListener.pause = false;
        }
    }
}
