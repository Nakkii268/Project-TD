using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceDisableState : AllianceState, IState
{
    public AllianceDisableState(AllianceSMManager allianceSMManager) : base(allianceSMManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        AllianceSMManager.Alliance.AllianceVisual.PlayDisableAnim();
    }
    public override void Exit()
    {
        base.Exit();
    }
    protected override void AddCallBack()
    {
        AllianceSMManager.Alliance.OnUnitDead += Alliance_OnUnitDead;
        AllianceSMManager.Alliance.StatusEffectHolder.OnEndDisable += StatusEffectHolder_OnEndDisable;
    }

    private void Alliance_OnUnitDead(object sender, EventArgs e)
    {
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceDeadState);
    }

    protected override void RemoveCallBack()
    {
        AllianceSMManager.Alliance.OnUnitDead -= Alliance_OnUnitDead;
        AllianceSMManager.Alliance.StatusEffectHolder.OnEndDisable -= StatusEffectHolder_OnEndDisable;

    }
    private void StatusEffectHolder_OnEndDisable(object sender, System.EventArgs e)
    {
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceIdleState);

    }
}
