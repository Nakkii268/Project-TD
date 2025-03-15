using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/EffectTypeSkill")]

public class EffectTypeSkills : ActiveSkills
{
    public List<EffectDelay> effects;
   

    public override void SkillActivate(AllianceSkill User, List<GameObject> target)
    {
        for (int i = 0; i < effects.Count; i++)
        {
            User.StartCoroutine(DelayEffect(User, target, effects[i].effect, effects[i].delayTime));
        }
    }
    private void EffectComponent(AllianceSkill User, List<GameObject> target, StatusEffect effect)
    {
        if (skillTarget == SkillTarget.Enemy)
        {
            foreach (GameObject tg in target)
            {
                tg.TryGetComponent<StatusEffectHolder>(out StatusEffectHolder effectHolder);
                effectHolder.AddStatusEffect(tg,effect);
                
            }
        }
        else if (skillTarget == SkillTarget.Self)
        {
            StatusEffectHolder effectHolder = User.GetComponentInParent<StatusEffectHolder>();
            effectHolder.AddStatusEffect(User.gameObject, effect);
            
        }
    }
    private IEnumerator DelayEffect(AllianceSkill User, List<GameObject> target,StatusEffect effect,float time)
    {
        yield return new WaitForSeconds(time);
        EffectComponent(User, target, effect);
    }
}
[Serializable]
public class EffectDelay
{
    public StatusEffect effect;
    public float delayTime;
}