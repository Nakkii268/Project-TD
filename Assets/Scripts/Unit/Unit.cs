using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : ScriptableObject
{
    public string UnitID;
    public GameObject UnitPrefab;
    public string Name;
    public float Heath;
    public float Attack;
    public float AttackInterval;
    public float Defense;
    public float Resistance;
    public int Block;
}
[Serializable]// maybe save it for skill part
public enum DamageType
{
    PhysicDamage,
    MagicDamage,
    TrueDamage
}
