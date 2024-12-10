using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/ComplexSkill")]

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

    public override void SkillActivate(GameObject User, List<GameObject> target)
    {

        if(skillTarget == SkillTarget.Self && subTarget == SkillSubTarget.Enemy)
        {
            //dmg self
            User.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
            dmgTarget.ReceiveDamaged(SkillDmg, DamageType);
            //debuff target
            foreach (GameObject tg in target)
            {
                tg.TryGetComponent<StatusEffectHolder>(out StatusEffectHolder effectHolder);

                for (int i = 0; i < effects.Count; i++)
                {
                    effectHolder.AddStatusEffect(tg, effects[i]);
                }
            }
        }

        if (skillTarget == SkillTarget.Enemy && subTarget == SkillSubTarget.Self)
        {
            //dmg target
            foreach (GameObject tg in target)
            {
                tg.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
                dmgTarget.ReceiveDamaged(SkillDmg, DamageType);
            }

            //buff self
            User.TryGetComponent<StatusEffectHolder>(out StatusEffectHolder effectHolder);

            for (int i = 0; i < effects.Count; i++)
            {
                effectHolder.AddStatusEffect(User, effects[i]);
            }
        }
    }
}
