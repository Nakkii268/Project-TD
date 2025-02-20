using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceMeleeAttack : AllianceAttack
{
    protected override void Start()
    {
        base.Start();
    }
    public override void AttackPerform()
    {
        
        base.AttackPerform();
        
    }

    public override void Attack()
    {
        
        if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy)
        {
            foreach (GameObject tg in currentTarget)
            {
                tg.GetComponentInParent<IDamageable>().ReceiveDamaged(allyAttack, damageType);
            }

        }
        else if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance)
        {
            AllianceHealerUnit healScale = (AllianceHealerUnit)alliance.GetAllianceUnit();
            
            foreach (GameObject tg in currentTarget)
            {
                tg.GetComponentInParent<IHealable>().Heal(allyAttack);
            }

        }
       
       
    }
}
