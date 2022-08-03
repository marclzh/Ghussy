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

    private string bgmKey = "MusicVolume";
    private string sfxKey = "SoundEffectsVolume";

    public void Start()
    {
       
        float bgmVolume = PlayerPrefs.GetFloat(bgmKey, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(sfxKey, 0.5f);

        OnMusicSliderValueChange(bgmVolume);
        OnSoundEffectsSliderValueChange(sfxVolume); 
    }

    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
      
        // Update Graphics
        musicSliderText.text = ((int)(value * 100)).ToString();
        musicSlider.value = value;

        // Saves new value
        PlayerPrefs.SetFloat(bgmKey, musicVolume);

        AudioManager.Instance.UpdateMixerVolume();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;

        // Update Graphics
        soundEffectsSliderText.text = ((int)(value * 100)).ToString();
        soundEffectSlider.value = value;

        // Saves new value
        PlayerPrefs.SetFloat(sfxKey, soundEffectsVolume);


        AudioManager.Instance.UpdateMixerVolume();
    }
}