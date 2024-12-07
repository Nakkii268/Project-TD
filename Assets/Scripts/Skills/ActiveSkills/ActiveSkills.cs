using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkills : Skills
{
    public ChargeType ChargeType;
    public float SkillDuration;
    public List<StatusEffect> effects;
    public float SkillDmg;
    public DamageType DamageType;
    public override void SkillActivate(GameObject target)
    {
        if (skillEffect == SkillEffect.StatusEffect)
        {
            target.TryGetComponent<StatusEffectHolder>(out StatusEffectHolder effectHolder);

            for (int i = 0; i < effects.Count; i++)
            {
                effectHolder.AddStatusEffect(target,effects[i]);
            }
        }else if(skillEffect == SkillEffect.DamagedDeal)
        {
            target.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
            dmgTarget.ReceiveDamaged(SkillDmg, DamageType);
        }else if(skillEffect == SkillEffect.Mix)
        {
            target.TryGetComponent<StatusEffectHolder>(out StatusEffectHolder effectHolder);
            target.TryGetComponent<IDamageable>(out IDamageable dmgTarget);

            dmgTarget.ReceiveDamaged(SkillDmg, DamageType);

            for (int i = 0; i < effects.Count; i++)
            {
                effectHolder.AddStatusEffect(target, effects[i]);
            }
        }
    }
}
