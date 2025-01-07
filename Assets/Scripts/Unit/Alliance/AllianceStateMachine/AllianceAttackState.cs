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


        Debug.Log("aatack in");
        AllianceSMManager.Alliance.AllianceAttack.AttackPerform();
        AllianceSMManager.Alliance.AllianceVisual.PlayAttackAnim();

    }
    public override void Exit() 
    { 
        base.Exit();
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
        AllianceSMManager.ChangeState(AllianceSMManager.AllianceIdleState);
    }

    public override void OnAnimationTransitionEvent()
    {
        AllianceSMManager.Alliance.AllianceAttack.Attack();

    }
}

