using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    public EnemyAttackState(EnemySMManager enemySMManager) : base(enemySMManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        EnemySMManager._enemy.EnemyAttack.PerformAttack();
        EnemySMManager._enemy.EnemyVisual.PlayAttackAnim();
    }

    public override void Exit()
    {
        base.Exit();
    }

  

    public override void OnAnimationTransitionEvent()
    {
        EnemySMManager._enemy.EnemyAttack.Attack();
    }

    public override void OnAnimationExitEvent()
    {
        if (EnemySMManager._enemy.IsBlocked)
        {
            EnemySMManager.ChangeState(EnemySMManager.EnemyIdleState);

        }

        EnemySMManager.ChangeState(EnemySMManager.EnemyMovingState);


    }

}
