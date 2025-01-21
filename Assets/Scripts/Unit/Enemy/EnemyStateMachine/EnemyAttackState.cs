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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnAnimationEnterEvent()
    {
        
    }

    public override void OnAnimationTransitionEvent()
    {
        EnemySMManager.Enemy.EnemyAttack.Attack(EnemySMManager.Enemy.EnemyAttackCollider.DetectEnenmy());
    }

    public override void OnAnimationExitEvent()
    {
        if (EnemySMManager.Enemy.IsBlocked)
        {
            EnemySMManager.ChangeState(EnemySMManager.EnemyIdleState);

        }

        EnemySMManager.ChangeState(EnemySMManager.EnemyMovingState);


    }

}
