using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameController _gameController;

    public void OnRestart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnExit()
    {
        SceneManager.LoadScene(1);
    }
}
