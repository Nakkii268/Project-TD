using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData 
{
    public string PlayerID;
    public string PlayerName;
    public int Diamond;
    public int Gold;
    public int Exp;
    public List<Item> Items;
    public List<CharacterModifyData> OwnedCharacter;
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
