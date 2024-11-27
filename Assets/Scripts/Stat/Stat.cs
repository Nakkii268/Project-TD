using System;

using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat 
{
    [SerializeField]private float BaseStat;
    [SerializeField] private List<StatModifier> Modifiers;
    public float Value { get
        {
            if (isDirty)
            {
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        } 
    }
    private bool isDirty = true;
    private float _value;
   public Stat(float baseStat)
    {
        BaseStat = baseStat;
        Modifiers = new List<StatModifier>();
        Modifiers.Sort(CompareModifierOrder);//sort by order, default is add flat then percent
       
   }
    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if(a.Order < b.Order) return -1;
        else if(a.Order > b.Order)return 1;
        return 0;
    }
    public bool RemoveAllModifiersFromSource(object source)
    {
        bool removed = false;
        for (int i = Modifiers.Count - 1; i >= 0; i--)
        {
            if(Modifiers[i].Source == source)
            {
                isDirty = true;
                removed = true;
                Modifiers.RemoveAt(i);
            }
        }
        return removed;
    }
    public void AddingModifier(StatModifier modifier) {
        isDirty = true;
        Modifiers.Add(modifier);
    }
    public bool RemovingModifier(StatModifier modifier)
    {
       if(Modifiers.Remove(modifier))
        {
            isDirty = true;
            return true;
        }
       return false;
    }

    private float CalculateFinalValue()
    {
        float finalValue = BaseStat;
        float sumPercentAdd = 0;
        for (int i = 0; i < Modifiers.Count; i++)
        {
            if (Modifiers[i].ModType  == StatModType.Flat)
            {
                finalValue += Modifiers[i].Value;

            }
            else if (Modifiers[i].ModType == StatModType.PercentAdd)
            {
                
                sumPercentAdd += Modifiers[i].Value;

                if (i+1 > Modifiers.Count || Modifiers[i+1].ModType != StatModType.PercentAdd)
                {
                    finalValue *= sumPercentAdd;
                }
            } 
            else if (Modifiers[i].ModType == StatModType.PercentMult)
            {
                finalValue *=1+ Modifiers[i].Value;
            }
        }
        return (float)Math.Round(finalValue, 1);
    }
}
