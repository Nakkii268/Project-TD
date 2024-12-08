using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class StatModifier 
{
    public float Value;
    public StatModType ModType;
    public int Order;
    public object Source; // know where is the source of modifier
    public StatModifier(float value, StatModType modType, int order, object source)
    {
        Value = value;
        ModType = modType;
        Order = order;
        Source = source;
    }
    public StatModifier(float value, StatModType modType) : this (value, modType, (int)modType,null) { }
    public StatModifier(float value, StatModType modType, int order) : this (value, modType, order,null) { }
    public StatModifier(float value, StatModType modType,object source) : this (value, modType, (int)modType,source) { }
}
public enum StatModType
{
    Flat,
    PercentAdd,
    PercentMult
}