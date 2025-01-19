using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttack
{
    [SerializeField] private Transform Projectile;
    protected override void PerformAttack()
    {
        base.PerformAttack();

    }

    public override void Attack(GameObject target)
    {
        target.GetComponentInParent<IDamageable>().ReceiveDamaged(attackDmg, damageType);

    }

}
