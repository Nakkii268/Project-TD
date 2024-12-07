using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkills : Skills
{
    public List<StatusEffect> effects;

    public override void SkillActivate(GameObject target)
    {
        target.TryGetComponent<StatusEffectHolder>(out StatusEffectHolder effectHolder);

        for (int i = 0; i < effects.Count; i++)
        {
            effectHolder.AddStatusEffect(target, effects[i]);
        }
    }
}
