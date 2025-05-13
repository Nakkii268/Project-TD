using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadData 
{
   

  
    public static PlayerData LoadCharacterData()
    {
       string savePath = Application.persistentDataPath + "/SaveLoadData.json";
       
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        }
        return null;
    }
    public static void SaveCharacterData(PlayerData data)
    {
        string savePath = Application.persistentDataPath + "/SaveLoadData.json";

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(json, savePath);
    }
    

    
}
