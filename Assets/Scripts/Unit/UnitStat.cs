using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStat : MonoBehaviour
{
   
    public Unit unit;
    [SerializeField] private int unitLevel;
    [SerializeField] private float currentHp;
    public Stat MaxHp;
    public Stat Attack;
    public Stat AttackInterval;
    public Stat Defense;
    public Stat Resistance;
    public Stat Block;
    public Vector2Int[] AttackRange;

    protected virtual void Start()
    {
        Initialized();
    }
    protected virtual  void Initialized()
    {
        
        MaxHp = new Stat(unit.Heath);
        Attack = new Stat(unit.Attack);
        AttackInterval = new Stat(unit.AttackInterval);
        Defense = new Stat(unit.Defense);
        Resistance = new Stat(unit.Resistance);
        
        Block = new Stat(unit.Block);
        AttackRange = unit.AttackRange;

    }


}
