using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceAttackState : AllianceState, IState
{
    public AllianceAttackState(AllianceSMManager allianceSMManager) : base(allianceSMManager)
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
    protected override void RemoveCallBack()
    {
        base.RemoveCallBack();
        AllianceSMManager.Alliance.AllianceAttack.OnNoEnemy -= Alliance_OnNoEnemy;

    }

    private void Alliance_OnNoEnemy(object sender, System.EventArgs e)
    {
        if (AllianceSMManager.Alliance.AllianceSkill.IsSkillDuration)
        {
            AllianceSMManager.ChangeState(AllianceSMManager.AllianceSkillDuarationState);
        }
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceIdleState);
    }

    public override void OnAnimationExitEvent() 
    {
        if (AllianceSMManager.Alliance.AllianceSkill.IsSkillDuration)
        {
            AllianceSMManager.ChangeState(AllianceSMManager.AllianceSkillDuarationState);
        }
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceIdleState);
    }

    public override void OnAnimationTransitionEvent()
    {
        AllianceSMManager.Alliance.AllianceAttack.Attack();

    }
}

