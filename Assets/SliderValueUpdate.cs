using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FloatEvent : UnityEvent<float>
{
    internal void AddListener(FloatEvent onMusicVolumeSliderChanged)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Class that encapsulates and manages a single slider, inlcuding a label for text update
/// </summary>
public class SliderValueUpdate : MonoBehaviour
{

    public Slider slider;
    public TextMeshProUGUI valueText;
    public string stringFormat;

    public FloatEvent OnSliderValueChanged = new FloatEvent();

    private float _sliderValue;
    private bool _started = false;
    public float SliderValue
    {
        get
        {
            return slider.value;
        }
        set
        {
            var newValue = Mathf.Clamp01(value);
            // this has to be called here as it may be set before the Start method of each MonoBehvaiour is called
            OnUnderlyingSliderValueChanged(newValue);
            slider.value = newValue;
            if (!_started)
            {
                OnSliderValueChanged.Invoke(newValue);
            }
        }
    }

    private void Start()
    {
        slider.onValueChanged.AddListener(OnUnderlyingSliderValueChanged);
        _started = true;
    }

    private void OnUnderlyingSliderValueChanged(float newValue)
    {
        valueText.text = string.Format(stringFormat, Mathf.RoundToInt(newValue*100f));
        OnSliderValueChanged.Invoke(newValue);
    }
}
