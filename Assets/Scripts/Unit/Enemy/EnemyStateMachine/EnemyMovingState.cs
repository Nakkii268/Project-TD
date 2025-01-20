using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingState : EnemyState
{
    public EnemyMovingState(EnemySMManager enemySMManager) : base(enemySMManager)
    {
    }


    public override void Enter()
    {
        base.Enter();
        EnemySMManager.Enemy.IsMoving = true;
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        Move();
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
