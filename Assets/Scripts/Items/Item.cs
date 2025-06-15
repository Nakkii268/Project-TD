using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    public string ItemID;
    public string ItemName;
    public Sprite ItemSprite;
    public string Description;
    public ItemCategory Category;
}

public enum ItemCategory
{
    Currency,
    Consumable,
    Material
}