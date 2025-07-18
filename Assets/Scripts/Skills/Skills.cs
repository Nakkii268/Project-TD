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
    [Header("SKill VFX")]
    public VFXData SkillVFX;
    public ParticleSystem SkillHitVFX = null;
    public virtual void SkillActivate(AllianceSkill User)
    {

    }
    
}

