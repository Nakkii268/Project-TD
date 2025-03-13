using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="Skill/DamageTypeSkill")]
public class DamageTypeSkills : ActiveSkills
{
    public float SkillDmg;
    public DamageType DamageType;
    public float delayTime;

    public override void SkillActivate(AllianceSkill User, List<GameObject> target)
    {
        User.StartCoroutine(DelayDamage(User.gameObject,target,delayTime));    
    }

    private void Damage(GameObject User, List<GameObject> target)
    {
        if (skillTarget == SkillTarget.Enemy)
        {
            foreach (GameObject tg in target)
            {
                tg.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
                dmgTarget.ReceiveDamaged(SkillDmg, DamageType);
            }
        }
        else if (skillTarget == SkillTarget.Self)
        {

            User.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
            dmgTarget.ReceiveDamaged(SkillDmg, DamageType);
        }
    }

    private IEnumerator DelayDamage(GameObject User, List<GameObject> target,float time)
    {
        yield return new WaitForSeconds(time);
        Damage(User, target);
    }
}
