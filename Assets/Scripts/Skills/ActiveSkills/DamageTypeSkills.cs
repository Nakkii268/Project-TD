using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="Skill/DamageTypeSkill")]
public class DamageTypeSkills : ActiveSkills
{
    public float SkillDmgScale;
    public DamageType DamageType;
    public float delayTime;

    public override void SkillActivate(AllianceSkill User, List<GameObject> target=null)
    {
        User.StartCoroutine(DelayDamage(User,target,delayTime));    
    }
    
    private void Damage(AllianceSkill User, List<GameObject> target)
    {
        if (skillTarget == SkillTarget.Enemy)
        {
            foreach (GameObject tg in target)
            {
                tg.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmgScale* User.alliance.Stat.Attack.Value, DamageType);
            }
        }
        else if (skillTarget == SkillTarget.Self)
        {

            User.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmgScale * User.alliance.Stat.Attack.Value, DamageType);
        }
    }

    private IEnumerator DelayDamage(AllianceSkill User, List<GameObject> target,float time)
    {
        yield return new WaitForSeconds(time);
        Damage(User, target);
    }
}
