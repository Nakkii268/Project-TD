using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Skill/AttackEnhance")]
public class AttackEnhanceSkills : ActiveSkills
{
    public float ScaleUp;
    public ParticleSystem AttackVFX;
    public ParticleSystem HitVFX;
    public List<StatusEffect> BuffEffect;
    

    public override void SkillActivate(AllianceSkill User )
    {
        
        for (int i = 0; i < BuffEffect.Count; i++)
        {
            {

                StatusEffectHolder effectHolder = User.GetComponentInParent<StatusEffectHolder>();
                effectHolder.AddStatusEffect(User.gameObject, BuffEffect[i]);
            }

        }
    }
    
}
