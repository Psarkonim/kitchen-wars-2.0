using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("—сылки на UI элементы")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle fiveLevelToggle;

    private const string MusicKey = "MusicVolume";
    private const string SfxKey = "SoundVolume";
    private const string FullscreenKey = "Fullscreen";

    private void Start()
    {
        float savedMusic = PlayerPrefs.GetFloat(MusicKey, 0.75f);
        float savedSFX = PlayerPrefs.GetFloat(SfxKey, 0.75f);
        bool savedFullscreen = PlayerPrefs.GetInt(FullscreenKey, 1) == 1;
        bool onFiveLevelChanger = PlayerPrefs.GetInt("Current level", 5) == 5;


        if (musicSlider != null) musicSlider.value = savedMusic;
        if (sfxSlider != null) sfxSlider.value = savedSFX;
        if (fullscreenToggle != null) fullscreenToggle.isOn = savedFullscreen;
        if (fiveLevelToggle != null) fiveLevelToggle.isOn = onFiveLevelChanger;


        OnMusicSliderChanged(savedMusic);
        OnSFXSliderChanged(savedSFX);
        ApplyFullscreen(savedFullscreen);

        if (musicSlider != null) musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(OnSFXSliderChanged);
        if (fullscreenToggle != null) fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
        if (fiveLevelToggle != null) fiveLevelToggle.onValueChanged.AddListener(OnFiveLevelChanger);
    }


    private void OnFiveLevelChanger(bool isFiveLevel)
    {
        PlayerPrefs.SetInt("Current level", isFiveLevel ? 5 : 1);
    }

    private void OnMusicSliderChanged(float value)
    {
        PlayerPrefs.SetFloat(MusicKey, value); 
    }


    private void OnSFXSliderChanged(float value)
    {
        PlayerPrefs.SetFloat(SfxKey, value); 
    }


    private void OnFullscreenToggleChanged(bool isFullscreen)
    {
        PlayerPrefs.SetInt(FullscreenKey, isFullscreen ? 1 : 0);
        ApplyFullscreen(isFullscreen);
    }

    private void ApplyFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }
}