using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceSkill : MonoBehaviour
{
    
    public Skills OnUseSkill;
    public Alliance alliance;
    public GameObject target;
    [SerializeField]private float curSkillPoint;

    private void Start()
    {
        if(OnUseSkill.skillType == SkillType.Active)
        {
            ActiveSkills skill = (ActiveSkills)OnUseSkill;
            curSkillPoint = 0;

           

            if(skill.ChargeType == ChargeType.Defensive)
            {
                alliance.OnGetHit += Alliance_OnGetHit; ;
            }
            else if(skill.ChargeType == ChargeType.Offensive)
            {
                alliance.AllianceAttack.OnAttackPerform += AllianceAttack_OnAttackPerform;
            }else if(skill.ChargeType == ChargeType.Auto)
            {
                InvokeRepeating("AutoRegenSkillPoint", 1f, 1f);
            }
        }
        
    }

    private void OnDisable()
    {
        CancelInvoke("AutoRegenSkillPoint");
    }
    private void AllianceAttack_OnAttackPerform(object sender, System.EventArgs e)
    {
        SkillPointRecover(1);
    }

    private void Alliance_OnGetHit(object sender, System.EventArgs e)
    {
        SkillPointRecover(1);
    }

    

    public void OnSkilluse()
    {
        OnUseSkill.SkillActivate(alliance.gameObject, target);
        curSkillPoint = 0;
    }
    private void SkillPointRecover(float amout)
    {
        ActiveSkills skill = (ActiveSkills)OnUseSkill;

        if (curSkillPoint >= skill.SkillPoint) return;
        curSkillPoint += amout;
    }
    
    private void AutoRegenSkillPoint()
    {
        SkillPointRecover(1);
    }
}
