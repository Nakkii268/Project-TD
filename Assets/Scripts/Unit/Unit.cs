using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : ScriptableObject
{
    [Header("Basic Info")]
    public string UnitID;
    public GameObject UnitPrefab;
    public string Name;
    //change in runtime
    [Header("Stat")]
    public float Heath;
    public float Attack;
    public float AttackInterval;
    public float Defense;
    public float Resistance;
    public int Block;

    //
    [Header("VFX")]
    public ParticleSystem AttackVfx;
    public ParticleSystem HitVfx;
}
[Serializable]// maybe save it for skill part
public enum DamageType
{
    PhysicDamage,
    MagicDamage,
    TrueDamage
}
