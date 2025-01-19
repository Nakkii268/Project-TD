using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceMeleeAttack : AllianceAttack
{
    public override void AttackPerform()
    {
        
        base.AttackPerform();
        
    }

    public override void Attack()
    {
        List<GameObject> target = GetTarget();
        if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy)
        {
            foreach (GameObject tg in target)
            {
                tg.GetComponentInParent<IDamageable>().ReceiveDamaged(allyAttack, damageType);
            }

        }
        else if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance)
        {
            AllianceHealerUnit healScale = (AllianceHealerUnit)alliance.GetAllianceUnit();
            
            foreach (GameObject tg in target)
            {
                tg.GetComponentInParent<IHealable>().Heal(allyAttack);
            }

        }
       
       
    }
}
