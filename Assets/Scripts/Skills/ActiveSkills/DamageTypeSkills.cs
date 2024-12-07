using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTypeSkills : ActiveSkills
{
    public float SkillDmg;
    public DamageType DamageType;

    public override void SkillActivate(GameObject User, GameObject target)
    {
        if (skillTarget == SkillTarget.Enemy)
        {
            target.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
            dmgTarget.ReceiveDamaged(SkillDmg, DamageType);
        }
        else if(skillTarget == SkillTarget.Self)
        {
            User.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
            dmgTarget.ReceiveDamaged(SkillDmg, DamageType);
        }
    }
}
