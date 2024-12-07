using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComplexSkills : ActiveSkills
{
    public List<StatusEffect> effects;

    public float SkillDmg;
    public DamageType DamageType;

    public SkillSubTarget subTarget;
    //dmg to maintarget, effect to sub target
    //case:
    // dmg self, debuff enemy
    //buff self, dmg enemy

    public override void SkillActivate(GameObject User, GameObject target)
    {
        if(skillTarget == SkillTarget.Self && subTarget == SkillSubTarget.Enemy)
        {
            User.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
            dmgTarget.ReceiveDamaged(SkillDmg, DamageType);

            target.TryGetComponent<StatusEffectHolder>(out StatusEffectHolder effectHolder);

            for (int i = 0; i < effects.Count; i++)
            {
                effectHolder.AddStatusEffect(target, effects[i]);
            }
        }
        if (skillTarget == SkillTarget.Enemy && subTarget == SkillSubTarget.Self)
        {
            target.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
            dmgTarget.ReceiveDamaged(SkillDmg, DamageType);

            User.TryGetComponent<StatusEffectHolder>(out StatusEffectHolder effectHolder);

            for (int i = 0; i < effects.Count; i++)
            {
                effectHolder.AddStatusEffect(target, effects[i]);
            }
        }
    }
}
