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
        alliance.AllianceVisual.RotateToTarget(currentTarget[0]);
        if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Enemy)
        {
            foreach (GameObject tg in currentTarget)
            {
                tg.GetComponentInParent<IDamageable>().ReceiveDamaged(alliance.Stat.Attack.Value, damageType);
                LevelManager.instance.ParticleManager.SlashParticle(tg);

            }

        }
        else if (Alliance.GetAllianceUnit().UnitTarget == UnitTarget.Alliance)
        {
           
            
            foreach (GameObject tg in currentTarget)
            {
                tg.GetComponentInParent<IHealable>().Heal(alliance.Stat.Attack.Value);
            }

        }
       
    }
}
