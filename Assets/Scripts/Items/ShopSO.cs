using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Shop")]
public class ShopSO : ScriptableObject
{
    public List<ShopSlotData> Slots;
    public bool Renewable;

    public void RenewItem()
    {
        if (Renewable)
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                Slots[i].AvailableQtt = Slots[i].MaxQtt;
            }
        }
    }
}
[Serializable]
public class ShopSlotData
{
    public ShopCategory Category;
    public Item Item;
    public int MaxQtt;
    public int AvailableQtt;
    public Currency Currency;
    public int Price;
    public int Quantity;
}
[Serializable]
public class ShopItemSave
{
    public string ItemID;
    public int Available;
    public ShopItemSave(string ItemID, int Available)
    {
        this.ItemID = ItemID;
        this.Available = Available;
    }
}
[Serializable]
public enum ShopCategory
{
    CharacterShop,
    MatetialShop
}