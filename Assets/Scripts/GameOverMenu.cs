using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameController _gameController;
    public InputManager _inputManager;

    private void Start()
    {
        _inputManager.OnKeyPressed.AddListener(OnKeyPressed);
    }

    private void OnKeyPressed(KeyAction keyAction)
    {
        if (keyAction == KeyAction.Jump)
        {
            OnRestart();
        }
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnExit()
    {
        SceneManager.LoadScene(1);
    }
}
