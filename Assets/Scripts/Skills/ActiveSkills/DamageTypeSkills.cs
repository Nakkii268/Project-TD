using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Skill/DamageTypeSkill")]
public class DamageTypeSkills : ActiveSkills
{
    public float SkillDmg;
    public DamageType DamageType;

    public override void SkillActivate(GameObject User, List<GameObject> target)
    {
        Debug.Log("dmged");
        if (skillTarget == SkillTarget.Enemy)
        {
            foreach (GameObject tg in target)
            {
                tg.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
                dmgTarget.ReceiveDamaged(SkillDmg, DamageType);
            }
        }
        else if(skillTarget == SkillTarget.Self)
        {

            User.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
            dmgTarget.ReceiveDamaged(SkillDmg, DamageType);
        }
    }
}
