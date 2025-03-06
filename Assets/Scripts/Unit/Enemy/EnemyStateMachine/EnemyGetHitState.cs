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
        EnemySMManager._enemy.EnemyVisual.PlayGetHitAnim();

    }

    public override void Exit()
    {
        base.Exit();
    }
    
  
    public override void OnAnimationExitEvent()
    {
        if (EnemySMManager._enemy.IsMoving && !EnemySMManager._enemy.IsBlocked)
        {
            EnemySMManager.ChangeState(EnemySMManager.EnemyMovingState);
            return;
        }
        EnemySMManager.ChangeState(EnemySMManager.EnemyIdleState);
    }
    
    
}
