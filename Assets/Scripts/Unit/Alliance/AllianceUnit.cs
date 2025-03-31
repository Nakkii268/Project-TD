using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Unit/Alliance")]

public class AllianceUnit : Unit
{
    public UnitRarity Rarity;
    public Sprite unitSprite;
    public Sprite unitPotrait;
    public List<Vector2> AttackRange;

    public AllianceType type;
    public int UnitDp;
    public float RedeployTime;

    public LayerMask EnemyType;
    public UnitTarget UnitTarget;
    public TargetCount TargetCount;
    public DamageType DamageType;
    public string GetAllianceType() { return type.ToString(); }
    public CharacterClass UnitClass;
    

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
    Alliance
    
}

