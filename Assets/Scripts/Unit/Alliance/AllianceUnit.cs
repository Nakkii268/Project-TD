using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Unit/Alliance")]

public class AllianceUnit : Unit
{
    [Header("Ally Info")]
    public UnitRarity Rarity;
    public Sprite unitSprite;
    public Sprite unitPotrait;
    public Sprite unitSplashArt;
    public Sprite UnitRangeVisualized;

    [Header("Advanced info")]
    public List<Vector2> AttackRange;

    public AllianceType type;
    public int UnitDp;
    public float RedeployTime;

    public LayerMask EnemyType;
    public UnitTarget UnitTarget;
    public TargetCount TargetCount;
    public DamageType DamageType;
    public CharacterClass UnitClass;
    [Header("Upgrade")]
    //change in runtime
    #region ChangeInRunTime
    [Range(0,2)]public int LimitBreak;
    [Range(1,90)]public int Level;
    //
    #endregion

    public string GetAllianceType() { return type.ToString(); }
    
    

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

