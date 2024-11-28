using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Unit/Alliance")]
public class AllianceUnit : Unit
{
    public GameObject prefab;
    public int UnitDp;
    public Sprite unitSprite;
    public AllianceType type;
    public float RedeployTime;
    public LayerMask[] EnemyType;
    public string GetAllianceType() { return type.ToString(); }
}
[Serializable]

    public enum AllianceType
{
        Ground,
        HighGround
    }

