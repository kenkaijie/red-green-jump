using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuManager : MonoBehaviour
{
    public GameController gameController;
    public GameMusicManager musicManager;
    private UserSettings _settings;

    public UserSettingMonoBehaviour userSettingsManager;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    public GameObject settingsMenu;
    private GameSettingsMenu _settingMenuLayer;

    public Queue<GameObject> menus = new Queue<GameObject>();

    public void Start()
    {
        _settings = userSettingsManager.ReadSettings();
        _settingMenuLayer = settingsMenu.GetComponent<GameSettingsMenu>();
        _settingMenuLayer.EffectsVolume = _settings.EffectsVolume;
        _settingMenuLayer.MusicVolume = _settings.MusicVolume;
        _settingMenuLayer.OnMusicVolumeSliderChanged.AddListener(OnMusicVolumeSliderChanged);
        _settingMenuLayer.OnEffectsVolumeSliderChanged.AddListener(OnEffectsVolumeSliderChanged);
        EnableMenu(null);
        gameController.OnGameStateChanged.AddListener(OnGameStateChanged);
    }

    private void OnEffectsVolumeSliderChanged(float arg0)
    {
        musicManager.GameEffectsVolume = arg0;
        _settings.EffectsVolume = musicManager.GameEffectsVolume;
    }

    private void OnMusicVolumeSliderChanged(float arg0)
    {
        musicManager.GameMusicVolume = arg0;
        _settings.MusicVolume = musicManager.GameMusicVolume;
    }

    private List<GameObject> GetMenus()
    {
        return new List<GameObject>() { pauseMenu, gameOverMenu, settingsMenu };
    }

    private GameObject GetActiveMenu()
    {
        foreach (var menuItem in GetMenus())
        {
            if (menuItem.activeSelf)
            {
                return menuItem;
            }
        }
        return null;
    }

    private void EnableMenu(GameObject menu)
    {
        foreach (var menuItem in GetMenus())
        {
            if (menuItem == menu)
            {
                menuItem.SetActive(true);
            }
            else
            {
                menuItem.SetActive(false);
            }
        }
    }

    private void OnGameStateChanged(GameStateType newState)
    {
        if (newState == GameStateType.Paused)
        {
            EnableMenu(pauseMenu);
        }
    }

    public void GotoSettings()
    {
        menus.Enqueue(GetActiveMenu());
        EnableMenu(settingsMenu);
    }

    public void GotoGameOver()
    {
        menus.Enqueue(GetActiveMenu());
        EnableMenu(gameOverMenu);
    }

    public void GotoPauseMenu()
    {
        menus.Enqueue(GetActiveMenu());
        EnableMenu(pauseMenu);
    }

    public void BackButton()
    {
        userSettingsManager.WriteSettings(_settings);
        GameObject previousMenu = menus.Dequeue();
        EnableMenu(previousMenu);
    }

    public void ButtonResume()
    {
        EnableMenu(null);
        menus.Clear();
        gameController.SetPauseState(false);
    }

    public void ButtonRestart()
    {
        SceneManager.LoadScene(1);    
    }

    public void ButtonExitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
