using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AllianceUnit : Unit
{
    public GameObject prefab;
    public int UnitDp;
    public Sprite unitSprite;
    public AllianceType type;
    public float RedeployTime;
    public LayerMask[] EnemyType;
    public string GetAllianceType() { return type.ToString(); }
    public Class unitClass;

    public virtual void ApplyClassBuff() { }
}
[Serializable]

    public enum AllianceType
{
        Ground,
        HighGround
    }

