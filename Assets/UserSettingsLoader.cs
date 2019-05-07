using System.IO;
using UnityEngine;

public static class UserSettingsLoader
{
    public static void SaveSettings(UserSettings data, string path)
    {
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(path))
        {
            streamWriter.Write(jsonString);
        }
    }

    public static UserSettings LoadSettings(string path)
    {
        using (StreamReader streamReader = File.OpenText(path))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<UserSettings>(jsonString);
        }
    }
}
