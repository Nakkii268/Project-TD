using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceStat : UnitStat
{

    [SerializeField] private Alliance alliance;
  
    public Stat RedeployTime;
    

    protected override void Start()
    {
        Initialized();
    }
    protected override void Initialized()
    {
        
         unit = alliance.GetAllianceUnit();
         AllianceUnit allyUnit = (AllianceUnit)unit;
        
        MaxHp = new Stat(allyUnit.Heath);
        Attack = new Stat(allyUnit.Attack);
        AttackInterval = new Stat(allyUnit.AttackInterval);
        Defense = new Stat(allyUnit.Defense);
        Resistance = new Stat(allyUnit.Resistance);
        RedeployTime = new Stat(allyUnit.RedeployTime);
        Block = new Stat(allyUnit.Block);
        AttackRange = allyUnit.AttackRange;

    }
}
