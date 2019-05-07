using System;

[Serializable]
public class UserSettings
{
    public readonly int SchemaVersion = 1;
    public float MusicVolume;
    public float EffectsVolume;

    public static UserSettings GetDefault()
    {
        return new UserSettings()
        {
            MusicVolume = 1f,
            EffectsVolume = 1f
        };
    }
}
