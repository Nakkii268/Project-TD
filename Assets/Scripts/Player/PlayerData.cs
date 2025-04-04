using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlayerData : ScriptableObject
{
    public string PlayerID;
    public string PlayerName;
    public int Diamond;
    public int Gold;
    public int Exp;
    public List<Item> Items;
}
