using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkills : Skills
{
    public List<StatusEffect> effects;

    public override void SkillActivate(AllianceSkill User)
    {
        if (skillTarget == SkillTarget.Self)//passive only buff self
        {
            StatusEffectHolder effectHolder= User.GetComponentInParent<StatusEffectHolder>();
            for (int i = 0; i < effects.Count; i++)
            {
                effectHolder.AddStatusEffect(User.gameObject, effects[i]);
            }
        }
       
    }
}
