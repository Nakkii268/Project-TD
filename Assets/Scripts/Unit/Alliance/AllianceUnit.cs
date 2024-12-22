using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AllianceUnit : Unit
{
    public GameObject prefab;
    public Sprite unitSprite;
    public List<Vector2> AttackRange;

    public AllianceType type;
    public int UnitDp;
    public float RedeployTime;

    public LayerMask EnemyType;
    public UnitTarget UnitTarget;
    public string GetAllianceType() { return type.ToString(); }
    public Class UnitClass;
    public StatusEffect ClassBuff;

    public virtual void ApplyClassBuff(GameObject unit) { }
}
[Serializable]

    public enum AllianceType
{
        Ground,
        HighGround
    }
public enum UnitTarget
{
    Enemy,
    Alliance,
    Both
}

