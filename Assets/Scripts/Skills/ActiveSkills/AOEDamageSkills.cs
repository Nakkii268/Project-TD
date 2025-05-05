using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


[CreateAssetMenu(menuName = "Skill/amageTypeSkill/AOE")]
public class AOEDamageSkills : DamageTypeSkills
{
    public Vector2 Range;//width-height
    public LayerMask TargetLayer;
    public override void SkillActivate(AllianceSkill User, List<GameObject> target)
    {
        
    }
    public override void SkillActivate(AllianceSkill User)
    {
        User.StartCoroutine(DelayDamage(User, delayTime));
    }
    private void Damage(AllianceSkill User)
    {
       
        float centerx = ((RangeSwap(User.alliance.direction).x / 2 + .5f) * User.alliance.direction).x + (User.alliance.UnitPos.x);
        float centery = ((RangeSwap(User.alliance.direction).y / 2 + .5f) * User.alliance.direction).y + (User.alliance.UnitPos.y);
        Debug.Log(User.alliance.UnitPos + "poss");
        Debug.Log(User.alliance.direction + "dir");
        Debug.Log(centerx + "centerXX");
        Debug.Log(centery + "centerYY");
        Debug.Log((Range.x/2+centerx) + "max");
        Debug.Log((Range.x/2-centerx) + "max");
        Collider2D[] hits = Physics2D.OverlapBoxAll(new Vector2(centerx,centery), RangeSwap(User.alliance.direction), 0,TargetLayer,-5,5);
        Debug.Log(hits.Length);

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
    private Vector2 RangeSwap(Vector2 dir)
    {
        if (dir.x != 0)
        {
            return Range;
        }
        else
        {
            return new Vector2(Range.y,Range.x);
        }
    }
}
