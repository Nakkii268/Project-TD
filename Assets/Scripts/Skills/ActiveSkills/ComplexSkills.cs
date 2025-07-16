using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ComplexSkill")]

public class ComplexSkills : ActiveSkills
{
    public List<StatusEffect> effects;

    public float SkillDmg;
    public DamageType DamageType;

    public SkillSubTarget subTarget;
    public float DamageDelayTime;
    public float EffectDelayTime;
    
    //dmg to maintarget, effect to sub target
    //case:
    // dmg self, debuff enemy
    //buff self, dmg enemy

    public override void SkillActivate(AllianceSkill User)
    {

        LevelManager.instance.ParticleManager.SkillParticle(User.gameObject, SkillVFX, User.transform, User.alliance.GetVFXQuaternion());

        User.StartCoroutine(DelayStatus(User,EffectDelayTime));
        User.StartCoroutine(DelayDamage(User, DamageDelayTime));
       
    }
    public virtual void DamageComponent(AllianceSkill User)
    {
        if (skillTarget == SkillTarget.Self)
        {
            //dmg self
            User.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmg, DamageType);
            
            
        }
       

        if (skillTarget == SkillTarget.Enemy )
        {
            //dmg target
            if (GetTarget(User) != null)
            {
                GameObject target = GetTarget(User)[0].gameObject;
                 target.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmg, DamageType);
            }
           
               
        }
    }
    public virtual void EffectComponent(AllianceSkill User) 
    {
        if ( subTarget == SkillSubTarget.Enemy)
        {
            //debuff target

           StatusEffectHolder target= GetTarget(User)[0].GetComponentInParent<StatusEffectHolder>();

                for (int i = 0; i < effects.Count; i++)
                {
                target.AddStatusEffect(target.gameObject, effects[i]);
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
    public IEnumerator DelayDamage(AllianceSkill User, float time)
    {
        yield return new WaitForSeconds(time);
        DamageComponent(User);
    }
    public IEnumerator DelayStatus(AllianceSkill User, float time)
    {
        yield return new WaitForSeconds(time);
        EffectComponent(User);
    }
    
}
