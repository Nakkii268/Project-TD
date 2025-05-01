using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : EnemyAttack
{
    [SerializeField] private Sprite ProjectileSprite;
    [SerializeField] private Transform firePoint;
    public override void PerformAttack()
    {
        base.PerformAttack();

    }

    public override void Attack()
    {
        GameObject projectile = LevelManager.instance.projectilePool.GetProjectile();

        projectile.transform.position = firePoint.position;
        projectile.gameObject.SetActive(true);
        projectile.GetComponent<Projectile>().SetInfomation(m_Enemy.Stat.Attack.Value, damageType, m_Enemy.Unit.target, target, firePoint, ProjectileSprite, m_Enemy.Unit.HitVfx);

    }

}
