using UnityEngine;
using System.IO;

public static class GameSaveManager
{
    private static string savePath = Application.persistentDataPath + "/save.json";

    
    public static void SaveGame()
    {
        SaveData data = new SaveData();

        
        data.gold = NewMonoBehaviourScript.gold; 
        
       
        string json = JsonUtility.ToJson(data, true);

        
        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved to " + savePath);
    }

    
    public static void LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            
            NewMonoBehaviourScript.gold = data.gold;
            

            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("No save file found!");
        }
    }
}
