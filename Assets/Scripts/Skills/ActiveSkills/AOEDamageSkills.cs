using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[CreateAssetMenu(menuName = "Skill/amageTypeSkill/AOE")]
public class AOEDamageSkills : DamageTypeSkills
{
    public Vector2 Range;//width-height


    public override void SkillActivate(AllianceSkill User)
    {
        User.StartCoroutine(DelayDamage(User, delayTime));
    }
    private void Damage(AllianceSkill User)
    {
        Vector2 center = ((Range.x / 2 - .5f) * User.alliance.direction) + (User.alliance.UnitPos);
        Collider2D[] hits = Physics2D.OverlapBoxAll(center, Range, 0);
        for (int i = 0; i < hits.Length; i++)
        {
            hits[i].GetComponentInParent<IDamageable>().ReceiveDamaged(SkillDmgScale * User.alliance.Stat.Attack.Value, DamageType);
        }

    }

    private IEnumerator DelayDamage(AllianceSkill User, float time)
    {
        yield return new WaitForSeconds(time);
        Damage(User);
    }
}
