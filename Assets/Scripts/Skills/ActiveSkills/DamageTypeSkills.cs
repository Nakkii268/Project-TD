using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="Skill/DamageTypeSkill")]
public class DamageTypeSkills : ActiveSkills
{
    public float SkillDmgScale;
    public DamageType DamageType;
    public float DelayTime;
    public int TotalHits; //number of hit will strke
    public float DelayBetweenHits;// delay btw each hit
    public override void SkillActivate(AllianceSkill User, List<GameObject> target=null)
    {
        User.StartCoroutine(DelayDamage(User,target,DelayTime,TotalHits,DelayBetweenHits));    
    }
    
    private void Damage(AllianceSkill User, List<GameObject> target)
    {
        LevelManager.instance.ParticleManager.SkillParticle(User.gameObject, SkillVFX, User.transform, User.alliance.GetVFXQuaternion());

        if (skillTarget == SkillTarget.Enemy)
        {
            foreach (GameObject tg in target)
            {
                tg.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmgScale* User.alliance.Stat.Attack.Value, DamageType);
                Debug.Log("hits");
            }
        }
        else if (skillTarget == SkillTarget.Self)
        {

            User.GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmgScale * User.alliance.Stat.Attack.Value, DamageType);
        }
    }

    private IEnumerator DelayDamage(AllianceSkill User, List<GameObject> target,float time,int hits,float delay)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < hits; i++)
        {
            Damage(User, target);
            yield return new WaitForSeconds(delay);
        }
    }
}
