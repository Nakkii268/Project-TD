using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]


public enum SkillType
{
    Active,
    Passive
}
public enum SkillActiveType
{
    ManualUse,
    AutoUse
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
    StatEffects,
    Mix,
    OnHitEffects
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
