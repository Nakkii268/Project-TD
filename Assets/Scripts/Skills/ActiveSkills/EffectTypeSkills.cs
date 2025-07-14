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

    public override void SkillActivate(AllianceSkill User, List<GameObject> target = null)
    {
        Debug.Log("acitive");
        for (int i = 0; i < effects.Count; i++)
        {
            Debug.Log("add effect");
            User.StartCoroutine(DelayEffect(User, target, effects[i].effect, effects[i].delayTime,Delay,SkillDuration));
        }
    }
    private void EffectComponent(AllianceSkill User, List<GameObject> target, StatusEffect effect)
    {
        LevelManager.instance.ParticleManager.SkillParticle(User.gameObject, SkillVFX, User.transform, User.alliance.GetVFXQuaternion());

        if (skillTarget == SkillTarget.Enemy)
        {
            foreach (GameObject tg in target)
            {
                StatusEffectHolder holder =  tg.GetComponentInParent<StatusEffectHolder>();
                holder.AddStatusEffect(tg,effect);
                
            }
        }
        else if (skillTarget == SkillTarget.Self)
        {
            StatusEffectHolder effectHolder = User.GetComponentInParent<StatusEffectHolder>();
            effectHolder.AddStatusEffect(User.gameObject, effect);
            
        }
    }
    private IEnumerator DelayEffect(AllianceSkill User, List<GameObject> target, StatusEffect effect, float time,float delay,float duration)
    {
        duration = SkillDuration;
        yield return new WaitForSeconds(time);
        while (duration > 0) {
            EffectComponent(User, target, effect);
            if (SkillDuration < 99)
            {
                SkillDuration -= Delay;

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