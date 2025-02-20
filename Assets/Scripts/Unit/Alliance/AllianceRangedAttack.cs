using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceRangedAttack : AllianceAttack
{
    [SerializeField] private Transform Projectile;
    [SerializeField] private Transform SpawnedProjectile;
    [SerializeField] private Transform EnhanceProjectile;
    [SerializeField] private Transform SpawnedEnhanceProjectile;
    public bool isEnhance;
    [SerializeField] private int hitCount=1;
    [SerializeField] private Transform firePoint;
   [SerializeField] private Transform fireTarget;

    protected override void Start()
    {
        base.Start();
        SpawnProjectiles();
    }
    public override void AttackPerform()
    {
        base.AttackPerform();
    }


    public void SpawnProjectiles()
    {
        SpawnedProjectile =  Instantiate(Projectile, firePoint.position, firePoint.rotation);
      //  SpawnedProjectile.gameObject.GetComponent<Projectile>().SetInfomation(allyAttack,damageType,alliance.GetAllianceUnit().UnitTarget,GetMinHpTarget())

    }
}
