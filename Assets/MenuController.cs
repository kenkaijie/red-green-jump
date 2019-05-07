using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public BackgroundManager _backgroundManager;
    // Start is called before the first frame update
    void Start()
    {
        _backgroundManager.SetMotionState(true);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    public  void OnSettingsButtonClick()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnQuitGame()
    {
        Application.Quit(0);
    }
}
