using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceMeleeAttack : AllianceAttack
{
    public override void AttackPerform()
    {
        Debug.Log("attack");
        if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy)
        {
            //attack enemy

        }
        else if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance)
        {
            //heal alliance

        }
        else if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Both)
        {
            //do both

        }
        base.AttackPerform();
        
    }
}
