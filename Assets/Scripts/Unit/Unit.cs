using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : ScriptableObject
{
    public string UnitID;
    public string Name;
    public float Heath;
    public float Attack;
    public float AttackInterval;
    public float Defense;
    public float Resistance;
    public float Block;
    public Vector2Int[] AttackRange;
}
[Serializable]// maybe save it for skill part
public enum DamageType
{
    PhysicDamage,
    MagicDamage,
    TrueDamage
}
