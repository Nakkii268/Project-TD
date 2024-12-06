using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : ScriptableObject
{
    public string Name;
    public float SkillPoint;
    public string Description;
    public SkillEffect skillEffect;
    
    public virtual void SkillActivate(GameObject target)
    {

    }
}

public enum ChargeType
{
    Offensive,
    Defensive,
    Auto
}
public enum SkillEffect
{
    DamagedDeal,
    StatusEffect,
    Mix
}