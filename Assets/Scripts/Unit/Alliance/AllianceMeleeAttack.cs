using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceMeleeAttack : AllianceAttack
{
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
        alliance.AllianceVisual.RotateToTarget(GetTarget()[0]);
        if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy)
        {
            foreach (GameObject tg in GetTarget())
            {
                tg.GetComponentInParent<IDamageable>().ReceiveDamaged(alliance.Stat.Attack.Value, damageType);
                LevelManager.instance.ParticleManager.HitParticle(tg, alliance.GetAllianceUnit().HitVfx);


            }

        }
        else if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance)
        {
           
            
            foreach (GameObject tg in GetTarget())
            {
                
                tg.GetComponentInParent<IHealable>().Heal(alliance.Stat.Attack.Value);
            }

        }
        LevelManager.instance.ParticleManager.AttackParticle(this.gameObject, alliance.GetAllianceUnit().AttackVfx,vfxPos);

    }
    public override void EnhanceAttack(float scaleUp,ParticleSystem vfx,ParticleSystem hitVfx)
    {
        Debug.Log("caleed");
        alliance.AllianceVisual.RotateToTarget(GetTarget()[0]);
        if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy)
        {
            foreach (GameObject tg in GetTarget())
            {
                tg.GetComponentInParent<IDamageable>().ReceiveDamaged(alliance.Stat.Attack.Value * scaleUp, damageType);
                LevelManager.instance.ParticleManager.HitParticle(tg,hitVfx);


            }

        }
        else if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance)
        {


            foreach (GameObject tg in GetTarget())
            {

                tg.GetComponentInParent<IHealable>().Heal(alliance.Stat.Attack.Value*scaleUp);
            }

        }
        LevelManager.instance.ParticleManager.AttackParticle(this.gameObject, vfx, vfxPos);
    }
}
