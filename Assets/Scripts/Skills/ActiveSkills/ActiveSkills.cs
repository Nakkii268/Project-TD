using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkills : Skills
{
    public ChargeType ChargeType;
    public float SkillDuration;
    public float SkillPoint;
    public bool CanAttack;
    public SkillActiveType ActiveType;

    [Header("Skill Range")]
    public Vector2 Range;
    public LayerMask TargetLayer;
    public bool IsAroundUser;
    public override void SkillActivate(AllianceSkill User)
    {
        
    }
    public Vector2 RangeSwap(Vector2 dir)
    {
        if (dir.x != 0)
        {
            return Range;
        }
        else
        {
            return new Vector2(Range.y, Range.x);
        }
    }
    public Collider2D[] GetTarget(AllianceSkill User)
    {
        float centerx = 0;
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
        return Physics2D.OverlapBoxAll(new Vector2(centerx, centery), RangeSwap(User.alliance.direction), 0, TargetLayer, -5, 5);
    }
}


