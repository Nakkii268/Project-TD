using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CreateAssetMenu(menuName ="Skill/DamageTypeSkill")]
public class DamageTypeSkills : ActiveSkills
{
    public float SkillDmgScale;
    public DamageType DamageType;
    public float DelayTime;
    public int TotalHits; //number of hit will strke
    public float DelayBetweenHits;// delay btw each hit
    public override void SkillActivate(AllianceSkill User)
    {
        User.StartCoroutine(DelayDamage(User,DelayTime,TotalHits,DelayBetweenHits));    
    }
    
    private void Damage(AllianceSkill User)
    {
        LevelManager.instance.ParticleManager.SkillParticle(User.gameObject, SkillVFX, User.transform, User.alliance.GetVFXQuaternion());

        if (skillTarget == SkillTarget.Enemy)
        {
            if (GetTarget(User) != null)
            {
                GameObject target = GetTarget(User)[0].gameObject;
                LevelManager.instance.ParticleManager.SkillHitParticle(target,SkillHitVFX);
                target.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmgScale * User.alliance.Stat.Attack.Value, DamageType);

            }


        }
        else if (skillTarget == SkillTarget.Self)
        {

            LevelManager.instance.ParticleManager.SkillHitParticle(User.gameObject, SkillHitVFX);
            User.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmgScale * User.alliance.Stat.Attack.Value, DamageType);

        }
    }

    private IEnumerator DelayDamage(AllianceSkill User,float time,int hits,float delay)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < hits; i++)
        {
            Damage(User);
            yield return new WaitForSeconds(delay);
        }
    }
}
