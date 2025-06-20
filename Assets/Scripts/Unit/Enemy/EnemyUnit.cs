using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Unit/Enemy")]

public class EnemyUnit : Unit
{
    public float Speed;
    public bool CanBlock;
    public EnemyType type;
    public DamageType damageType;
    public float AttackRange;
    public UnitTarget target;
    public LayerMask targetlayer;
}
public enum EnemyType
{
    Ground,
    Air
}