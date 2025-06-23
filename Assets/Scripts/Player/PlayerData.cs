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
    public List<Progress> PlayerProgress;
    public List<ShopItemSave> ShopItemsData;
    public List<QuestData> PlayerQuestData;
    public PlayerData() { }
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
    public SaveItemData(string itemId, int quantity)
    {
        ItemId = itemId;
        Quantity = quantity;
    }
}
[Serializable] 
public class LineUpData
{
    public int Index;
    public string UnitId;
    public int SkillIndex;
    public LineUpData(int index, string unitId, int skillIndex)
    {
        Index = index;
        UnitId = unitId;
        SkillIndex = skillIndex;
    }
}
[Serializable]
public class QuestData
{
    public string QuestID;
    public string QuestProgress;
    public QuestData(string id, string p)
    {
        QuestID = id;
        QuestProgress = p;
    }
}  