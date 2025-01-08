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
        base.AddCallBack();
        AllianceSMManager.Alliance.StatusEffectHolder.OnEndDisable += StatusEffectHolder_OnEndDisable;
    }
    protected override void RemoveCallBack()
    {
        base.RemoveCallBack();
        AllianceSMManager.Alliance.StatusEffectHolder.OnEndDisable -= StatusEffectHolder_OnEndDisable;

    }
    private void StatusEffectHolder_OnEndDisable(object sender, System.EventArgs e)
    {
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceIdleState);

    }
}
