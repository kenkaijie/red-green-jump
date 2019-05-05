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
    /// Game is about to resume
    /// </summary>
    Commencing,
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

    public float BaseMoveSpeed = -5f;

    public float GlobalGameMoveSpeed;

    public bool IsMoving()
    {
        return Mathf.Abs(GlobalGameMoveSpeed) > 0f;
    }

    private float _gameRunTime = 0f;
    public int GameScore { get; set; }

    public GameModeType GameMode;
    public GameStateType GameState;

    public GameObject GameOverScreen;
    public InputManager inputManager;
    public OnGameStateChangedEvent OnGameStateChanged = new OnGameStateChangedEvent();
    public GameObject CountdownTextField;

    // Difficulty Scaling Variables
    public float DifficultyScale;
    public float DifficultyRampRatePerSecond = 1f;
    public float MaxDifficultyScale = 200f;

    private void Start()
    {
        GlobalGameMoveSpeed = BaseMoveSpeed;
        TransitionGameState(GameStateType.Idle);
        ResetScore();
        StartCoroutine(CountdownResume());
        GameOverScreen.SetActive(false);
    }

    IEnumerator CountdownResume()
    {
        TransitionGameState(GameStateType.Commencing);
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
        TransitionGameState(GameStateType.Running);
    }

    private void FixedUpdate()
    {
        if (GameState == GameStateType.Running)
        {
            _gameRunTime += Time.fixedDeltaTime;
            GameScore = Mathf.RoundToInt(_gameRunTime);
            DifficultyScale = Mathf.Min(MaxDifficultyScale, GameScore * DifficultyRampRatePerSecond);
            GlobalGameMoveSpeed = BaseMoveSpeed * (1f + DifficultyScale / 100f);
        }
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

    public void ResetScore()
    {
        GameScore = 0;
        _gameRunTime = 0f;
        DifficultyScale = 0f;
    }

    public void TriggerGameOver()
    {
        // show game over screen
        TransitionGameState(GameStateType.Finish);
        GlobalGameMoveSpeed = 0f;
        GameOverScreen.SetActive(true);
    }
}
