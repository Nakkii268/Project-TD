using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : ScriptableObject
{
    public string UnitID;
    public string Name;
    public Stat Heath;
    public Stat Damaged;
    public Stat Defense;
    public Stat Resitan;
    public Stat Block;
    public Vector2Int[] AttackRange;
}
