using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;

public class GameSettingsMenu : MonoBehaviour
{
    // Public for References
    public SliderValueUpdate musicVolumeSlider;
    public SliderValueUpdate effectsVolumeSlider;

    public FloatEvent OnMusicVolumeSliderChanged = new FloatEvent();
    public FloatEvent OnEffectsVolumeSliderChanged = new FloatEvent();

    // Public Properties
    public float MusicVolume
    {
        get
        {
            return musicVolumeSlider.SliderValue;
        }
        set
        {
            musicVolumeSlider.SliderValue = value;
            // Need to manually invoke, as the listener has not been binded
            if (!_started)
            {
                OnMusicVolumeSliderChanged.Invoke(musicVolumeSlider.SliderValue);
            }
        }
    }

    public float EffectsVolume
    {
        get
        {
            return effectsVolumeSlider.SliderValue;
        }
        set
        {
            effectsVolumeSlider.SliderValue = value;
            // Need to manually invoke, as the listener has not been binded
            if (!_started)
            {
                OnEffectsVolumeSliderChanged.Invoke(effectsVolumeSlider.SliderValue);
            }
        }
    }

    private bool _started = false;

    void Start()
    {
        musicVolumeSlider.OnSliderValueChanged.AddListener(OnInternalMusicVolumeSliderChanged);
        effectsVolumeSlider.OnSliderValueChanged.AddListener(OnInternalEffectsVolumeSliderChanged);
        _started = true;
    }

    private void OnInternalEffectsVolumeSliderChanged(float arg0)
    {
        OnEffectsVolumeSliderChanged.Invoke(arg0);
    }

    private void OnInternalMusicVolumeSliderChanged(float arg0)
    {
        OnMusicVolumeSliderChanged.Invoke(arg0);
    }
}

