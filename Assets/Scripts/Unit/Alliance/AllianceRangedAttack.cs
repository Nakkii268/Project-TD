using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceRangedAttack : AllianceAttack
{
    [SerializeField] private Transform Projectile;
    [SerializeField] private Transform EnhanceProjectile;
    public bool isEnhance;
    [SerializeField] private int hitCount=1;
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


    public void SpawnProjectiles()
    {

    }
}
