using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/EffectTypeSkill")]

public class EffectTypeSkills : ActiveSkills
{
    public List<StatusEffect> effects;

    public override void SkillActivate(GameObject User, List<GameObject> target)
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
            User.TryGetComponent<StatusEffectHolder>(out StatusEffectHolder effectHolder);

            for (int i = 0; i < effects.Count; i++)
            {
                effectHolder.AddStatusEffect(User, effects[i]);
            }
        }
    }
}
