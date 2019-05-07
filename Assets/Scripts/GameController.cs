using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    /// Game is about to start
    /// </summary>
    Commencing,
    /// <summary>
    /// Running the game!
    /// </summary>
    Running,
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

    public GameObject HUDComponent;
    public GameObject GameOverScreen;
    public InputManager inputManager;
    public OnGameStateChangedEvent OnGameStateChanged = new OnGameStateChangedEvent();
    public GameObject CountdownTextField;

    // Parallax Background Scroller
    public BackgroundManager _backgroundManager;

    // User Settings
    public UserSettingMonoBehaviour UserSettings;

    // Audio Settings
    public GameMusicManager AudioManager;

    // Difficulty Scaling Variables
    public float DifficultyScale;
    public float DifficultyRampRatePerSecond = 1f;
    public float MaxDifficultyScale = 200f;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        var settings = UserSettings.ReadSettings();
        AudioManager.GameEffectsVolume = settings.EffectsVolume;
        AudioManager.GameMusicVolume = settings.MusicVolume;
        GlobalGameMoveSpeed = BaseMoveSpeed;
        TransitionGameState(GameStateType.Idle);
        ResetScore();

        StartCoroutine(CountdownResume());

        _backgroundManager.SetMotionState(true);
        GameOverScreen.SetActive(false);
        HUDComponent.SetActive(true);
        inputManager.OnKeyPressed.AddListener(OnKeyPressed);
    }

    private void OnKeyPressed(KeyAction key)
    {
        if (key == KeyAction.Pause)
        {
            if (GameState == GameStateType.Running)
            {
                SetPauseState(true);
            }
        }
    }

    public void SetPauseState(bool doPause)
    {
        if (doPause)
        {
            TransitionGameState(GameStateType.Paused);
            GlobalGameMoveSpeed = 0f;
            Time.timeScale = 0f;
            _backgroundManager.SetMotionState(false);
        }
        else
        {
            _backgroundManager.SetMotionState(true);
            Time.timeScale = 1f;
            GlobalGameMoveSpeed = BaseMoveSpeed * (1f + DifficultyScale / 100f);
            TransitionGameState(GameStateType.Running);
        }
    }

    private IEnumerator CountdownResume()
    {
        TransitionGameState(GameStateType.Commencing);
        CountdownTextField.SetActive(true);
        TextMeshProUGUI textfield = CountdownTextField.GetComponent<TextMeshProUGUI>();
        Animator textfieldAnimator = CountdownTextField.GetComponent<Animator>();

        textfield.text = "3";
        yield return new WaitForSeconds(1f);

        textfield.text = "2";
        yield return new WaitForSeconds(1f);

        textfield.text = "1";
        yield return new WaitForSeconds(1f);

        textfield.text = "JUMP!";
        textfieldAnimator.SetTrigger("jumpIn");
        yield return new WaitForSeconds(0.6f);

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
        _backgroundManager.SetMotionState(false);
        GameOverScreen.SetActive(true);
        HUDComponent.SetActive(false);
    }
}
