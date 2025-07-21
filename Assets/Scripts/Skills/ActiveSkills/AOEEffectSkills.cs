using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skill/AOEEffectTypeSkill")]

public class AOEEffectSkills : EffectTypeSkills
{
    public override void SkillActivate(AllianceSkill User)
    {
        for (int i = 0; i < effects.Count; i++)
        {

            User.StartCoroutine(DelayEffect(User, effects[i].effect, effects[i].delayTime, Delay, SkillDuration));
        }
    }

    protected override void EffectComponent(AllianceSkill User, StatusEffect effect)
    {

        //it AOE so it only ally or enemy, no self buff here and the gettarget with target layer already handle with which is target so just let it be
        LevelManager.instance.ParticleManager.SkillEffectParticle(User.gameObject, SkillVFX.Particle,SkillDuration);
        Debug.Log("Spawn SKill");
        Collider2D[] targets = GetTarget(User);
        for (int i = 0; i < targets.Length; i++)
        {
            StatusEffectHolder holder = targets[i].gameObject.GetComponentInParent<StatusEffectHolder>();
            LevelManager.instance.ParticleManager.SkillHitParticle(targets[i].gameObject, SkillHitVFX);
            Debug.Log("Spawn hit"+i);

            holder.AddStatusEffect(targets[i].gameObject, effect);

        }
    }

}
