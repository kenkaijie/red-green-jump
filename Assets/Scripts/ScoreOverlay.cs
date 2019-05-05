using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreOverlay : MonoBehaviour
{
    public string FormattedString;
    private TextMeshProUGUI _textComponent;
    public GameController gamecontroller;
    // Start is called before the first frame update
    void Start()
    {
        _textComponent = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _textComponent.text = string.Format(FormattedString, gamecontroller.GameScore);
    }
}
