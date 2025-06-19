using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LimitBreakData")]
public class LimitBreakSO : ScriptableObject
{
   
    public List<ItemsData> MaterialsRequired;
    public List<StatusEffect> BuffGain;
}

[Serializable]
public class ItemsData
{
    public Item Item;
    public int Quantity;

    public ItemsData(Item material,int qtt)
    {
        Item = material; 
        Quantity = qtt;
    }
}
