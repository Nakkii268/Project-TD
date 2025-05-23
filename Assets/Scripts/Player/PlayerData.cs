using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    public string PlayerID;
    public string PlayerName;
    public List<SaveItemData> Items;
    public List<CharacterModifyData> OwnedCharacter;
    public List<LineUpData> LineUp;
    public Progress PlayerProgress;
    public Progress LastCompleteStage;
   
}

[Serializable]
public class CharacterModifyData
{
    public string Id;
    public int Level;
    public int LimitBreak;

    public CharacterModifyData(string id, int level, int lb)
    {
        this.Id = id;
        this.Level = level;
        this.LimitBreak = lb;

    }
}
[Serializable] 
public class SaveItemData
{
    public string ItemId;
    public int Quantity;
}
[Serializable] 
public class LineUpData
{
    public int Index;
    public string UnitId;
}