using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills : ScriptableObject
{
    public string Name;
    public float SkillPoint;
    public string Description;
    
    public virtual void SkillActivate()
    {

    }
}
