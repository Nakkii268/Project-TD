using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceSkillAttackState : AllianceState
{
    public AllianceSkillAttackState(AllianceSMManager allianceSMManager) : base(allianceSMManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AllianceSMManager.Alliance.AllianceAttack.AttackPerform();
        AllianceSMManager.Alliance.AllianceVisual.PlayAttackAnim();
    }
    public override void Exit() 
    { 
        base.Exit(); 
    }
 
 


    protected override void AddCallBack()
    {
        base.AddCallBack();
        AllianceSMManager.Alliance.AllianceAttack.OnNoEnemy += Alliance_OnNoEnemy;
    }

    private void Alliance_OnNoEnemy(object sender, EventArgs e)
    {
        if (AllianceSMManager.Alliance.AllianceSkill.IsSkillDuration)
        {
            AllianceSMManager.ChangeState(AllianceSMManager.AllianceSkillDuarationState);
        }
    }

    protected override void RemoveCallBack()
    {
        base.RemoveCallBack();
        AllianceSMManager.Alliance.AllianceAttack.OnNoEnemy -= Alliance_OnNoEnemy;

    }
    public override void OnAnimationTransitionEvent()
    {
        AttackEnhanceSkills skill = (AttackEnhanceSkills)AllianceSMManager.Alliance.AllianceSkill.OnUseSkill;
        AllianceSMManager.Alliance.AllianceAttack.EnhanceAttack(skill.ScaleUp,skill.AttackVFX,skill.HitVFX);

    }

    public override void OnAnimationExitEvent()
    {
        if (AllianceSMManager.Alliance.AllianceSkill.IsSkillDuration)
        {
            AllianceSMManager.ChangeState(AllianceSMManager.AllianceSkillDuarationState);
            return;
        }
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceIdleState);
        AllianceSMManager.Alliance.AllianceVisual.RotateToDirection(AllianceSMManager.Alliance.direction);
    }
}
