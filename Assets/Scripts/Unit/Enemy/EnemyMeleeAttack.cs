using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{


    public override void PerformAttack()
    {
        base.PerformAttack();

    }

    public override void Attack()
    {
         target.GetComponentInParent<IDamageable>().ReceiveDamaged(m_Enemy.Stat.Attack.Value, damageType);
        

    }
}
