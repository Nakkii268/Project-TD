using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceRangedAttack : AllianceAttack
{
    [SerializeField] private Sprite ProjectileVisual;
    public bool isEnhance;
    [SerializeField] private int hitCount=1;
    [SerializeField] private Transform firePoint;


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
        if (alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy)
        {
            for (int i = 0; i < GetTarget().Count; i++)
            {
                AllianceProjectile projectile = LevelManager.instance.projectilePool.AllyPool.GetPooledObject();
                alliance.AllianceVisual.RotateToTarget(GetTarget()[i]);

                projectile.transform.position = firePoint.position;
                projectile.gameObject.SetActive(true);
                projectile.SetInfomation(alliance.Stat.Attack.Value, damageType, alliance.GetAllianceUnit().UnitTarget, GetTarget()[i], firePoint, ProjectileVisual, alliance.GetAllianceUnit().HitVfx);
            }
        }
        else
        {
            for (int i = 0; i < GetMinHpTarget().Count; i++)
            {
                AllianceProjectile projectile = LevelManager.instance.projectilePool.AllyPool.GetPooledObject();

                alliance.AllianceVisual.RotateToTarget(GetMinHpTarget()[i]);

                projectile.transform.position = firePoint.position;
                projectile.gameObject.SetActive(true);
                projectile.GetComponent<Projectile>().SetInfomation(alliance.Stat.Attack.Value, damageType, alliance.GetAllianceUnit().UnitTarget, GetMinHpTarget()[i], firePoint, ProjectileVisual, alliance.GetAllianceUnit().HitVfx);
            }
        }
    }

    
}
