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
    public Item Item;
    public int MaxQtt;
    public int AvailableQtt;
    public Currency Currency;
    public int Price;
    public int Quantity;
}