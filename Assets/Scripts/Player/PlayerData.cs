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
    public int Diamond;
    public int Gold;
    public int Exp;
    public List<ItemsData> Items;
    public List<CharacterModifyData> OwnedCharacter;
   // public List<AllianceUnit> OwnedCharacterSO;
   public bool IsHaveItem(string id)
    {
       for(int i = 0; i<Items.Count; i++)
        {
            if (Items[i].Material.ItemID == id) return true;
        }
       return false;
    }
    public ItemsData GetItem(string id)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Material.ItemID == id) return Items[i];
        }
        return null;
    }
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
