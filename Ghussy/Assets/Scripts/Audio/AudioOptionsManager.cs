using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioOptionsManager : MonoBehaviour
{
    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }

    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI soundEffectsSliderText;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectSlider;

    public void Start()
    {
        if (SaveManager.instance.hasLoaded)
        {
            SaveData currentSaveData = SaveManager.instance.activeSave;
            OnMusicSliderValueChange(currentSaveData.musicVolume);
            OnSoundEffectsSliderValueChange(currentSaveData.soundEffectsVolume);
        } 
        else
        {
            OnMusicSliderValueChange(.5f);
            OnSoundEffectsSliderValueChange(.5f);    
        }
    }

    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
      
        // Update Graphics
        musicSliderText.text = ((int)(value * 100)).ToString();
        musicSlider.value = value;

        // Saves new value
        SaveData currentSaveData = SaveManager.instance.activeSave;
        currentSaveData.musicVolume = musicVolume;

        AudioManager.Instance.UpdateMixerVolume();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;

        // Update Graphics
        soundEffectsSliderText.text = ((int)(value * 100)).ToString();
        soundEffectSlider.value = value;

        // Saves new value
        SaveData currentSaveData = SaveManager.instance.activeSave;
        currentSaveData.soundEffectsVolume = soundEffectsVolume;


        AudioManager.Instance.UpdateMixerVolume();
    }
}