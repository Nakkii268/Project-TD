using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGetHitState : EnemyState
{
    public EnemyGetHitState(EnemySMManager enemySMManager) : base(enemySMManager)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    
  
    public override void OnAnimationExitEvent()
    {
        if (EnemySMManager.Enemy.IsMoving && !EnemySMManager.Enemy.IsBlocked)
        {
            EnemySMManager.ChangeState(EnemySMManager.EnemyMovingState);
            return;
        }
        EnemySMManager.ChangeState(EnemySMManager.EnemyIdleState);
    }
    
    
}
