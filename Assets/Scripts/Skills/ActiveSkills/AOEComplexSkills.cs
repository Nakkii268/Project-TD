using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skill/ComplexSkill/AOE")]
public class AOEComplexSkills : ComplexSkills
{
   

    public override void SkillActivate(AllianceSkill User)
    {
        LevelManager.instance.ParticleManager.SkillParticle(User.gameObject, SkillVFX, User.transform, User.alliance.GetVFXQuaternion());

        User.StartCoroutine(DelayStatus(User, EffectDelayTime));
        User.StartCoroutine(DelayDamage(User, DamageDelayTime));
    }
    public override void DamageComponent(AllianceSkill User)
    {
        if (skillTarget == SkillTarget.Self)
        {
            //dmg self
            LevelManager.instance.ParticleManager.SkillHitParticle(User.gameObject, SkillHitVFX);

            User.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmg, DamageType);


        }


        if (skillTarget == SkillTarget.Enemy)
        {
            //dmg target
            foreach (Collider2D tg in GetTarget(User))
            {

                LevelManager.instance.ParticleManager.SkillHitParticle(tg.gameObject, SkillHitVFX);

                tg.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmg, DamageType);

            }
        }
    }
    public override void EffectComponent(AllianceSkill User)
    {
        if (subTarget == SkillSubTarget.Enemy)
        {
            //debuff target
            foreach (Collider2D tg in GetTarget(User))
            {
                StatusEffectHolder holder = tg.GetComponentInParent<StatusEffectHolder>();

                for (int i = 0; i < effects.Count; i++)
                {
                    holder.AddStatusEffect(tg.gameObject, effects[i]);
                }
            }
        }

        if (subTarget == SkillSubTarget.Self)
        {
            //buff self
            StatusEffectHolder holder = User.GetComponentInParent<StatusEffectHolder>();

            for (int i = 0; i < effects.Count; i++)
            {
                holder.AddStatusEffect(User.gameObject, effects[i]);
            }
        }
    }
    

   
}
