using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkills : Skills
{
    public List<StatusEffect> effects;

    public override void SkillActivate(AllianceSkill User, List<GameObject> target)
    {
        if (skillTarget == SkillTarget.Self)
        {
            StatusEffectHolder effectHolder= User.GetComponentInParent<StatusEffectHolder>();
            for (int i = 0; i < effects.Count; i++)
            {
                effectHolder.AddStatusEffect(User.gameObject, effects[i]);
            }
        }
        else
        {
            foreach (GameObject tg in target) {
                StatusEffectHolder effectHolder= tg.GetComponentInParent<StatusEffectHolder>();

            for (int i = 0; i < effects.Count; i++)
            {
                effectHolder.AddStatusEffect(tg, effects[i]);
            }
        } }
    }
}
