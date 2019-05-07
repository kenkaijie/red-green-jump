using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameMusicManager musicManager;

    public SliderValueUpdate musicVolumeSlider;
    public SliderValueUpdate effectsVolumeSlider;

    private UserSettings _settings;

    public UserSettingMonoBehaviour _userSettingsManager;

    void Start()
    {
        _userSettingsManager = gameObject.GetComponent<UserSettingMonoBehaviour>();
        _settings = _userSettingsManager.ReadSettings();
        musicVolumeSlider.SliderValue =_settings.MusicVolume;
        effectsVolumeSlider.SliderValue =_settings.EffectsVolume;
        OnMusicVolumeSliderChanged(_settings.MusicVolume);
        OnEffectsVolumeSliderChanged(_settings.EffectsVolume);
        musicVolumeSlider.OnSliderValueChanged.AddListener(OnMusicVolumeSliderChanged);
        effectsVolumeSlider.OnSliderValueChanged.AddListener(OnEffectsVolumeSliderChanged);
    }

    private void OnEffectsVolumeSliderChanged(float arg0)
    {
        musicManager.GameEffectsVolume = arg0;
    }

    private void OnMusicVolumeSliderChanged(float arg0)
    {
        musicManager.GameMusicVolume = arg0;

    }

    public void OnSliderBackButtonClick()
    {
        _settings.MusicVolume = musicVolumeSlider.SliderValue;
        _settings.EffectsVolume = effectsVolumeSlider.SliderValue;
        _userSettingsManager.WriteSettings(_settings);
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}

