using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : UnitStat
{
    public EnemyUnit eUnit;
   
    public float CurrentHp;
    public float AttackRange;
    public Stat Speed;
    public bool CanBlock;
    public EnemyType Type;

    private void Start()
    {
        Initialized();
    }

    protected override void Initialized()
    {
        
        unit = eUnit;
        EnemyUnit enemyUnit = (EnemyUnit)unit;
        MaxHp = new Stat(enemyUnit.Heath);
        Attack = new Stat(enemyUnit.Attack);
        AttackInterval = new Stat(enemyUnit.AttackInterval);
        Defense = new Stat(enemyUnit.Defense);
        Resistance = new Stat(enemyUnit.Resistance);
        Block = new Stat(enemyUnit.Block);
        AttackRange = enemyUnit.AttackRange;
        currentHp = MaxHp.Value;
        Speed = new Stat(enemyUnit.Speed);
        CanBlock = enemyUnit.CanBlock;
        Type = enemyUnit.type;
    }


}
