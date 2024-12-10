using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : UnitStat
{
    public EnemyUnit eUnit;
   
    public float CurrentHp;


  
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
    }


}
