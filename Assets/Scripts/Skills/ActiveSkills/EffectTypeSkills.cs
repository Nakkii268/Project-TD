using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/EffectTypeSkill")]

public class EffectTypeSkills : ActiveSkills
{
    public List<EffectDelay> effects;
    public float Delay; //delay time between each time apply buff/debuff  ( for continuous effect)

    public override void SkillActivate(AllianceSkill User)
    {
        
        for (int i = 0; i < effects.Count; i++)
        {
          
            User.StartCoroutine(DelayEffect(User, effects[i].effect, effects[i].delayTime,Delay,SkillDuration));
        }
    }
    protected virtual void EffectComponent(AllianceSkill User, StatusEffect effect)
    {

        if (skillTarget == SkillTarget.Enemy)
        {
            if (GetTarget(User) != null)
            {
                GameObject target = GetTarget(User)[0].gameObject;
                StatusEffectHolder holder = target.GetComponentInParent<StatusEffectHolder>();
                LevelManager.instance.ParticleManager.SkillEffectParticle(target, SkillVFX.Particle, SkillDuration);

                holder.AddStatusEffect(target, effect);
            }

            
                
            
        }
        else if (skillTarget == SkillTarget.Self)
        {
            StatusEffectHolder effectHolder = User.GetComponentInParent<StatusEffectHolder>();
            LevelManager.instance.ParticleManager.SkillEffectParticle(User.gameObject, SkillVFX.Particle, SkillDuration );

            effectHolder.AddStatusEffect(User.gameObject, effect);
            
        }
    }
    protected IEnumerator DelayEffect(AllianceSkill User, StatusEffect effect, float time,float delay,float duration)
    {
        float skillDuration = duration;
        yield return new WaitForSeconds(time);
        if(skillTarget== SkillTarget.Self) // called one time only if it sefl buff
        {
            EffectComponent(User, effect);
            yield break;
        }
        while (skillDuration > 0) {
            EffectComponent(User, effect);
            if (SkillDuration < 99)
            {
                skillDuration -= Delay;

            }
            yield return new WaitForSeconds(delay);
            
        } 
    }
}
[Serializable]
public class EffectDelay
{
    public StatusEffect effect;
    public float delayTime;
}