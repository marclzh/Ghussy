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
        SaveData currentSaveData = SaveManager.instance.activeSave;
        OnMusicSliderValueChange(currentSaveData.musicVolume);
        OnSoundEffectsSliderValueChange(currentSaveData.soundEffectsVolume);
    }

    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
      
        // Update Graphics
        musicSliderText.text = ((int)(value * 100)).ToString();
        musicSlider.value = value;

        AudioManager.Instance.UpdateMixerVolume();

        // Saves new value
        SaveData currentSaveData = SaveManager.instance.activeSave;
        currentSaveData.musicVolume = musicVolume;
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;

        // Update Graphics
        soundEffectsSliderText.text = ((int)(value * 100)).ToString();
        soundEffectSlider.value = value;

        AudioManager.Instance.UpdateMixerVolume();

        // Saves new value
        SaveData currentSaveData = SaveManager.instance.activeSave;
        currentSaveData.soundEffectsVolume = soundEffectsVolume;
    }
}