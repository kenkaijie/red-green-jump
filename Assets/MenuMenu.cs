using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMenu : MonoBehaviour
{
    public GameObject MainMenuView;
    public GameObject OptionsView;

    private void Start()
    {
        MainMenuView.SetActive(true);
        OptionsView.SetActive(false);
    }

    public void OnStartEndlessButton()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    public void OnQuitGame()
    {
        Application.Quit(0);
    }

    public void OnOptionsButton()
    {
        MainMenuView.SetActive(false);
        OptionsView.SetActive(true);
    }

    public void OnOptionsBackButton()
    {
        MainMenuView.SetActive(true);
        OptionsView.SetActive(false);
    }
}
