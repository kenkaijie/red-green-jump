using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameMusicManager : MonoBehaviour
{
    public AudioMixer _gameAudioMixer;

    private float _gameMusicVolume;
    private float _gameEffectsVolume;

    private float RawFloatToVolumeDB(float raw)
    {
        return 20f * Mathf.Log10(Mathf.Clamp(raw, 0.0001f, 1f));// 0.0001 = -80dB
    }

    public float GameMusicVolume
    {
        get
        {
            return _gameMusicVolume;
        }

        set
        {
            var newValue = Mathf.Clamp01(value);
            _gameMusicVolume = newValue;
            _gameAudioMixer.SetFloat("AmbientVolume", RawFloatToVolumeDB(newValue));
        }
    }

    public float GameEffectsVolume
    {
        get
        {
            return _gameEffectsVolume;
        }

        set
        {
            var newValue = Mathf.Clamp01(value);
            _gameEffectsVolume = newValue;
            _gameAudioMixer.SetFloat("EffectsVolume", RawFloatToVolumeDB(newValue));
        }
    }
}
