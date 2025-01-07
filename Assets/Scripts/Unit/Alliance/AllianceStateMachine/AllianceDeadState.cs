using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceDeadState : AllianceState
{
    public AllianceDeadState(AllianceSMManager allianceSMManager) : base(allianceSMManager)
    {
    }
    public override void Enter()
    {
        base.Enter();
        AllianceSMManager.Alliance.AllianceVisual.PlayDeadAnim();
    }
    public override void Exit()
    {
        base.Exit();
        //stop anim
    }



    public override void OnAnimationExitEvent()
    {
        AllianceSMManager.Alliance.Retreat(true);
    }



}
