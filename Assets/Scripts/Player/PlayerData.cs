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
