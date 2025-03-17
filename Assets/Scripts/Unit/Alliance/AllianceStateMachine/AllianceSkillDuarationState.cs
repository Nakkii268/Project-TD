using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceSkillDuarationState : AllianceState, IState
{
    public AllianceSkillDuarationState(AllianceSMManager allianceSMManager) : base(allianceSMManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AllianceSMManager.Alliance.AllianceVisual.PlayIdleAnim();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        if (!AllianceSMManager.Alliance.AllianceSkill.CanAttackInDuration) return;
        if (AllianceSMManager.Alliance.AllianceAttack.CanPerformAttack())
        {
            AllianceSMManager.ChangeState(AllianceSMManager.AllianceAttackState);
        }
    }

    protected override void AddCallBack()
    {
        base.AddCallBack();
        AllianceSMManager.Alliance.AllianceSkill.OnSkillEnd += AllianceSkill_OnSkillEnd;
    }
    protected override void RemoveCallBack()
    {
        base.RemoveCallBack();
        AllianceSMManager.Alliance.AllianceSkill.OnSkillEnd -= AllianceSkill_OnSkillEnd;

    }

    private void AllianceSkill_OnSkillEnd(object sender, System.EventArgs e)
    {
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceIdleState);
    }
}
