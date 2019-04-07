using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartEndlessButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnQuitGame()
    {
        Application.Quit(0);
    }
}
