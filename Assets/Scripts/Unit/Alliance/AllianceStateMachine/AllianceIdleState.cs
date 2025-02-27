using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllianceIdleState : AllianceState, IState
{
    public AllianceIdleState(AllianceSMManager allianceSMManager) : base(allianceSMManager)
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
        if (AllianceSMManager.Alliance.AllianceAttack.CanPerformAttack())
        {
            AllianceSMManager.ChangeState(AllianceSMManager.AllianceAttackState);
        }
    }
    public override void FixedUpdate()
    {
    }

    public override void OnAnimationEnterEvent()
    {
    }

    public override void OnAnimationExitEvent()
    {
    }

    public override void OnAnimationTransitionEvent()
    {
    }
}
