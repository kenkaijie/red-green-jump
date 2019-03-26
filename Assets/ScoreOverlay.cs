using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreOverlay : MonoBehaviour
{
    private Text _text;
    private GameController _gamecontroller;
    // Start is called before the first frame update
    void Start()
    {
        _gamecontroller = GameController.GetGameController();
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_gamecontroller.GameMode == GameType.Time)
        {
            _text.text = string.Format("Game Score: {0}", _gamecontroller.GameCollideScore);
        }
        else
        {
            _text.text = string.Format("Time {0}",TimeSpan.FromSeconds(_gamecontroller.GameTimeScore));
        }
        
    }
}
