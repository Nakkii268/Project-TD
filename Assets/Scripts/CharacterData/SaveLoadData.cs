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

    public static void ConvertToSO(PlayerDataSO newData,PlayerData data)
    {
        newData.ClearData();
        newData.PlayerID = data.PlayerID;
        newData.PlayerName = data.PlayerName;
        //item
        for (int i = 0; i < data.Items.Count; i++)
        {
            ItemsData it = new ItemsData(GameManager.Instance._resourceManager.GetItemById<Item>(data.Items[i].ItemId), data.Items[i].Quantity);
           
            newData.Items.Add(it);
        }
        //unit
        for (int i = 0; i < data.OwnedCharacter.Count; i++)
        {
            
            AllianceUnit un = GameManager.Instance._resourceManager.GetUnitById<AllianceUnit>(data.OwnedCharacter[i].Id);
            un.Level = data.OwnedCharacter[i].Level;
            un.LimitBreak = data.OwnedCharacter[i].LimitBreak;
           
            un.CalculateStat();
            newData.OwnedCharacter.Add(un);
        }
        newData.PlayerProgress = data.PlayerProgress;
        
        //line up
        for (int i = 0; i < data.LineUp.Count; i++)
        {
            newData.PlayerLineUp.Add(new LineUpSave(data.LineUp[i].Index, GameManager.Instance._resourceManager.GetUnitById<AllianceUnit>(data.LineUp[i].UnitId), data.LineUp[i].SkillIndex));
        }
        
    }
  


}
