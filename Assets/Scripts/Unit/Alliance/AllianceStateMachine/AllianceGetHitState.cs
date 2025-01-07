using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceGetHitState : AllianceState
{
    public AllianceGetHitState(AllianceSMManager allianceSMManager) : base(allianceSMManager)
    {
    }
    public override void Enter()
    {
        base.Enter();
        //play anim
        AllianceSMManager.Alliance.AllianceVisual.PlayGetHitkAnim();

    }
    public override void Exit()
    {
        base .Exit();
        //stopanim
    }
   

    public override void OnAnimationExitEvent()
    {
        if (AllianceSMManager.Alliance.AllianceSkill.IsSkillDuration)
        {
            AllianceSMManager.ChangeState(AllianceSMManager.AllianceSkillDuarationState);
        }
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceIdleState);
    }


}
