using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceSkill : MonoBehaviour
{
    public List<Skills> skills;
    public Skills OnUseSkill;
    public Alliance alliance;
    public GameObject target;
    public void OnSkilluse(Skills skill)
    {
        skill.SkillActivate(alliance.gameObject, target);
    }   
}
