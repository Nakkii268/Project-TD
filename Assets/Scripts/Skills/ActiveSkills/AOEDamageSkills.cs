using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using static UnityEngine.GraphicsBuffer;


[CreateAssetMenu(menuName = "Skill/amageTypeSkill/AOE")]
public class AOEDamageSkills : DamageTypeSkills
{
    
    public override void SkillActivate(AllianceSkill User)
    {
        User.StartCoroutine(DelayDamage(User, DelayTime,TotalHits,DelayBetweenHits));

    }
   
    private void Damage(AllianceSkill User)
    {
        

        float centerx =0;
        float centery = 0;
        if (IsAroundUser)
        {
            centerx = User.alliance.UnitPos.x;
            centery = User.alliance.UnitPos.y;
        }
        else
        {
            centerx = ((RangeSwap(User.alliance.direction).x / 2 + .5f) * User.alliance.direction).x + (User.alliance.UnitPos.x);
            centery = ((RangeSwap(User.alliance.direction).y / 2 + .5f) * User.alliance.direction).y + (User.alliance.UnitPos.y);
        }
        Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(centerx,centery), RangeSwap(User.alliance.direction), 0,TargetLayer,-5,5);
        

        for (int i = 0; i < hits.Length; i++)
        {
            LevelManager.instance.ParticleManager.SkillHitParticle(hits[i].gameObject, SkillHitVFX);

            hits[i].GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmgScale * User.alliance.Stat.Attack.Value, DamageType);
            Debug.Log("AOE hits");
        }

    }

   
    private IEnumerator DelayDamage(AllianceSkill User, float time,int hits, float delay)
    {
        yield return new WaitForSeconds(time);
        LevelManager.instance.ParticleManager.SkillParticle(User.gameObject, SkillVFX, User.transform, User.alliance.GetVFXQuaternion());

        for (int i = 0; i < hits; i++)
        {
            Damage(User);
            yield return new WaitForSeconds(delay);
        }
        
    }
   
}
