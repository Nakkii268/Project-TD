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
        
        AllianceSMManager.Alliance.AllianceVisual.PlayDeadAnim();
    }





    public override void OnAnimationExitEvent()
    {
        AllianceSMManager.Alliance.Retreat(true);
    }



}
