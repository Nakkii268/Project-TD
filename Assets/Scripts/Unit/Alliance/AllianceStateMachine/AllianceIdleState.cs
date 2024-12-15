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

        //play anim
    }
    public override void Exit()
    {
        //stopanim
    }
    public override void Update()
    {

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
