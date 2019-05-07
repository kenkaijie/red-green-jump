
using System.IO;
using UnityEngine;

public class UserSettingMonoBehaviour: MonoBehaviour
{
    private UserSettings _settings;
    private bool _cached = false;
    private string _settingDataPath = null;
    private string SettingDataPath
    {
        get
        {
            if (_settingDataPath is null)
            {
                _settingDataPath = Path.Combine(Application.persistentDataPath, "UserSettings.Json");
            }
            return _settingDataPath;
        }

        set
        {
            _settingDataPath = value;
        }
    }

    public void WriteSettings(UserSettings settings)
    {
        _settings = settings;
        _cached = true;
        Debug.Log(string.Format("Settings Write: MusicVol: {0}, EffectsVol: {1}", _settings.MusicVolume, _settings.EffectsVolume));
        UserSettingsLoader.SaveSettings(settings, SettingDataPath);
    }

    public UserSettings ReadSettings()
    {
        if (!_cached)
        {
            try
            {
                _settings = UserSettingsLoader.LoadSettings(SettingDataPath);
            }
            catch (IOException)
            {
                _settings = UserSettings.GetDefault();
            }
            _cached = true;
        }
        Debug.Log(string.Format("Settings Read: MusicVol: {0}, EffectsVol: {1}", _settings.MusicVolume, _settings.EffectsVolume));
        return _settings;
    }

}

