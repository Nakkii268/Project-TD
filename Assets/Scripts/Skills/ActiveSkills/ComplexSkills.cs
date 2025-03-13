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
    public float DamageDelayTime;
    public float EffectDelayTime;
    //dmg to maintarget, effect to sub target
    //case:
    // dmg self, debuff enemy
    //buff self, dmg enemy

    public override void SkillActivate(AllianceSkill User, List<GameObject> target)
    {
        User.StartCoroutine(DelayStatus(User,target,EffectDelayTime));
        User.StartCoroutine(DelayDamage(User, target, DamageDelayTime));
       
    }
    private void DamageComponent(AllianceSkill User, List<GameObject> target)
    {
        if (skillTarget == SkillTarget.Self)
        {
            //dmg self
            User.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmg, DamageType);
            
            
        }

        if (skillTarget == SkillTarget.Enemy )
        {
            //dmg target
            foreach (GameObject tg in target)
            {
                tg.TryGetComponent<IDamageable>(out IDamageable dmgTarget);
                dmgTarget.ReceiveDamaged(SkillDmg, DamageType);
            }  
        }
    }
    private void EffectComponent(AllianceSkill User, List<GameObject> target) {
        if ( subTarget == SkillSubTarget.Enemy)
        {
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

        if (subTarget == SkillSubTarget.Self)
        {
            //buff self
            StatusEffectHolder holder = User.GetComponentInParent<StatusEffectHolder>();

            for (int i = 0; i < effects.Count; i++)
            {
                holder.AddStatusEffect(User.gameObject, effects[i]);
            }
        }
    }
    private IEnumerator DelayDamage(AllianceSkill User, List<GameObject> target, float time)
    {
        yield return new WaitForSeconds(time);
        DamageComponent(User, target);
    }
    private IEnumerator DelayStatus(AllianceSkill User, List<GameObject> target, float time)
    {
        yield return new WaitForSeconds(time);
        EffectComponent(User, target);
    }
}
