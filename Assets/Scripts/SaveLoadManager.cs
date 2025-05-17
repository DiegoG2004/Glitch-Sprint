using System.IO;
using UnityEngine;

public static class SaveLoadManager
{
    //Make a folder named Saves
    private static string m_SaveFolder = Application.persistentDataPath + "/Saves";

    public static T Load<T>(string fileName)
    {
        //Combine the two stringsS
        string filePath = Path.Combine(m_SaveFolder, fileName);

        if (File.Exists(filePath))
        {
            string savedText = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(savedText);
        }
        else
        {
            //Create the file, tho not for now
            Debug.Log("File not found");
            return default;
        }
    }
    public static async void Save<T>(string fileName, T data)
    {
        //Create a save from idk what thing
        string filePath = Path.Combine(m_SaveFolder,fileName);
        string json = JsonUtility.ToJson(data, true);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        //Let the thingie save
        await File.WriteAllTextAsync(filePath, json);
    }
}
