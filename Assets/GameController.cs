using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum GameModeType
{
    Score,
    Time
}
public enum GameStateType
{
    /// <summary>
    /// Just entered the scene
    /// </summary>
    Idle,
    /// <summary>
    /// Running the game!
    /// </summary>
    Running,
    /// <summary>
    /// Pause has been requested
    /// </summary>
    Paused,
    /// <summary>
    /// Game Over
    /// </summary>
    Finish
}

public class OnGameStateChangedEvent : UnityEvent<GameStateType>
{

}

public class GameController: MonoBehaviour
{
    public int IncorrectCollideScore = -3;
    public int CorrectCollideScore = 1;

    public Vector2 GlobalGameMoveSpeed;

    private int _gameCollideScore;
    public int GameCollideScore { get => _gameCollideScore; set => _gameCollideScore = Math.Max(0, value); }
    public float GameTimeScore { get; set; }
    public GameModeType GameMode { get; internal set; }
    public GameStateType GameState { get; private set; }

    public InputManager inputManager;
    public OnGameStateChangedEvent OnGameStateChanged = new OnGameStateChangedEvent();
    public GameObject CountdownTextField;

    private void Start()
    {
        GlobalGameMoveSpeed = Vector2.zero;
        TransitionGameState(GameStateType.Idle);
        inputManager.OnKeyPressed.AddListener(OnKeyPressed);
        ResetScore();
        StartCoroutine(CountdownResume());
    }

    IEnumerator CountdownResume()
    {
        CountdownTextField.SetActive(true);
        Text textfield = CountdownTextField.GetComponent<Text>();
        textfield.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        textfield.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        textfield.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        textfield.text = "G";
        yield return new WaitForSecondsRealtime(0.5f);
        CountdownTextField.SetActive(false);
        StartGame();
    }

    public void StartGame()
    {
        TransitionGameState(GameStateType.Running);
        GlobalGameMoveSpeed = new Vector2(-11f, 0);
    }

    private void TransitionGameState(GameStateType newState)
    {
        if (GameState != newState)
        {
            Debug.Log("Transitioning game state from " + GameState + " -> " + newState);
            GameState = newState;
            OnGameStateChanged.Invoke(GameState);
        }
    }

    private void OnKeyPressed(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Escape:
                if (GameState == GameStateType.Running)
                {
                    PauseGame();
                }
                else if (GameState == GameStateType.Paused)
                {
                    UnpauseGame();
                    
                }
                break;
            default:
                // do nothing
                break;
        }
    }

    public void PauseGame()
    {
        TransitionGameState(GameStateType.Paused);
        GlobalGameMoveSpeed = Vector2.zero;
    }

    public void UnpauseGame()
    {
        StartCoroutine(CountdownResume());
    }

    public void ResetScore()
    {
        GameCollideScore = 0;
        GameTimeScore = 0f;
    }
}
