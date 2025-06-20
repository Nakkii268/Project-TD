using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceProjectile : Projectile
{
    protected override void Update()
    {
        base.Update();

    }
    public override void SetInfomation(float dmg, DamageType type, UnitTarget ut, GameObject tg, Transform s, Sprite visual, ParticleSystem hit)
    {
        damaged = dmg;
        damageType = type;
        targetUnit = ut;
        target = tg;
        source = s;
        projectileVisual.sprite = visual;
        hitParticle = hit;

    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform == target.transform)
        {
            DamagedTarget();
            gameObject.SetActive(false);
        }
    }

    protected override void DamagedTarget()
    {
        if (targetUnit == UnitTarget.Enemy)
        {
            target.GetComponentInParent<IDamageable>().ReceiveDamaged(damaged, damageType);

        }
        else if (targetUnit == UnitTarget.Alliance)
        {
            target.GetComponentInParent<IHealable>().Heal(damaged);

        }
        LevelManager.instance.ParticleManager.HitParticle(target, hitParticle);
    }
    protected override void MoveToTarget()
    {

        base.MoveToTarget();


    }
    protected override void RotateToTarget(GameObject target)
    {
        base.RotateToTarget(target);


    }
}
