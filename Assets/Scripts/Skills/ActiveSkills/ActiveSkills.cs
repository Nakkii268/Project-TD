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
    public override void SkillActivate(GameObject User, List<GameObject> target)
    {
        
    }
}


