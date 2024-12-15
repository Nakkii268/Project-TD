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
        //play animation
        //get dm value, dmg ttype
        //attack
    }
    public override void Exit() 
    { 
       //stop anim
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

