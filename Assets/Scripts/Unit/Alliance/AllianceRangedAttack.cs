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


    protected override void Start()
    {
        base.Start();
        //SpawnProjectiles();
    }
    public override void AttackPerform()
    {
        base.AttackPerform();
    }
    public override void Attack()
    {
        Transform SpawnedProjectile1 = Instantiate(Projectile, firePoint.position, Quaternion.identity);
        SpawnedProjectile1.gameObject.GetComponent<Projectile>().SetInfomation(allyAttack, damageType, alliance.GetAllianceUnit().UnitTarget, currentTarget[0],firePoint);

    }

    public void SpawnProjectiles()
    {
       //GameObject SpawnedProjectile =  Instantiate(Projectile, firePoint.position, Quaternion.identity);
       // SpawnedProjectile.gameObject.SetActive(false);

    }
}
