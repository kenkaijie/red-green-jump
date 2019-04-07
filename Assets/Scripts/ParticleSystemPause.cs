using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemPause : MonoBehaviour
{
    public GameController _gameController;
    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _gameController.OnGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnGameStateChanged(GameStateType state)
    {
        if (state == GameStateType.Running)
        {
            _particleSystem.Play(true);
        }
        else
        {
            _particleSystem.Pause(true);
        }
    }
}
