using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreOverlay : MonoBehaviour
{
    private Text _text;
    public GameController gamecontroller;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gamecontroller.GameMode == GameModeType.Score)
        {
            _text.text = string.Format("Game Score: {0}", gamecontroller.GameCollideScore);
        }
        else
        {
            _text.text = string.Format("Time {0}",TimeSpan.FromSeconds(gamecontroller.GameTimeScore));
        }
        
    }
}
