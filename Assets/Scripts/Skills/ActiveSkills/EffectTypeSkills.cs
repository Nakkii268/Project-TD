using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/EffectTypeSkill")]

public class EffectTypeSkills : ActiveSkills
{
    public List<StatusEffect> effects;
    public float delayTime;

    public override void SkillActivate(AllianceSkill User, List<GameObject> target)
    {
        
       User.StartCoroutine(DelayEffect(User, target,delayTime));
    }
    private void EffectComponent(AllianceSkill User, List<GameObject> target)
    {
        if (skillTarget == SkillTarget.Enemy)
        {
            foreach (GameObject tg in target)
            {
                tg.TryGetComponent<StatusEffectHolder>(out StatusEffectHolder effectHolder);

                for (int i = 0; i < effects.Count; i++)
                {
                    effectHolder.AddStatusEffect(tg, effects[i]);
                }
            }
        }
        else if (skillTarget == SkillTarget.Self)
        {
            StatusEffectHolder effectHolder = User.GetComponentInParent<StatusEffectHolder>();

            for (int i = 0; i < effects.Count; i++)
            {
                effectHolder.AddStatusEffect(User.gameObject, effects[i]);
            }
        }
    }
    private IEnumerator DelayEffect(AllianceSkill User, List<GameObject> target,float time)
    {
        yield return new WaitForSeconds(time);
        EffectComponent(User, target);
    }
}
