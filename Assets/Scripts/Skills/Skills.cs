using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : ScriptableObject
{
    public string Name;
    public string Description;
    public SkillType skillType;
    public SkillEffect skillEffect;
    public SkillTarget skillTarget;
    public virtual void SkillActivate(GameObject User,List<GameObject> target)
    {

    }
}
public enum SkillType
{
    Active,
    Passive
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
public enum SkillTarget
{
    Alliance,
    Self,
    Enemy
}
public enum SkillSubTarget
{
    None,
    Alliance,
    Self,
    Enemy
}