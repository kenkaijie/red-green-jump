using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum GameType
{
    Score,
    Time
}

public class GameController
{
    private static GameController instance;
    public static GameController GetGameController()
    {
        if (instance is null)
        {
            instance = new GameController();
        }
        return instance;
    }

    public static int IncorrectCollideScore = -3;
    public static int CorrectCollideScore = 1;

    public Vector2 GlobalGameMoveSpeed = new Vector2(-11f, 0);

    private int _gameCollideScore;
    public int GameCollideScore { get => _gameCollideScore; set => _gameCollideScore = Math.Max(0, value); }
    public float GameTimeScore { get; set; }
    public GameType GameMode { get; internal set; }

    private GameController()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        GameCollideScore = 0;
        GameTimeScore = 0f;
    }
}
