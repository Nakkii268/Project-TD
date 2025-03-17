using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceRangedAttack : AllianceAttack
{
    [SerializeField] private Sprite ProjectileVisual;
    
    [SerializeField] private Transform EnhanceProjectile;
    [SerializeField] private Transform SpawnedEnhanceProjectile;
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
       GameObject projectile =  LevelManager.instance.projectilePool.GetProjectile();
        
        projectile.transform.position = firePoint.position;
        projectile.gameObject.SetActive(true);
        projectile.GetComponent<Projectile>().SetInfomation(alliance.Stat.Attack.Value, damageType, alliance.GetAllianceUnit().UnitTarget, currentTarget[0],firePoint,ProjectileVisual);

    }

    
}
