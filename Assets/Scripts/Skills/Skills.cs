using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public string Description;
    public SkillType skillType;
    public SkillEffect skillEffect;
    public SkillTarget skillTarget;
    public bool TargetRequire;
    public Vector2[] SkillRange;
    public VFXData SkillVFX;
    public virtual void SkillActivate(AllianceSkill User,List<GameObject> target=null)
    {

    }
    
}

