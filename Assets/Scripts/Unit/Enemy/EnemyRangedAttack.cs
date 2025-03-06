using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttack
{
    [SerializeField] private Sprite ProjectileSprite;
    [SerializeField] private Transform firePoint;
    protected override void PerformAttack()
    {
        base.PerformAttack();

    }

    public override void Attack(GameObject target)
    {
        GameObject projectile = LevelManager.instance.projectilePool.GetProjectile();

        projectile.transform.position = firePoint.position;
        projectile.gameObject.SetActive(true);
        projectile.GetComponent<Projectile>().SetInfomation(attackDmg, damageType, m_Enemy.Unit.target, GetClosestTarget(), firePoint, ProjectileSprite);

    }

}
