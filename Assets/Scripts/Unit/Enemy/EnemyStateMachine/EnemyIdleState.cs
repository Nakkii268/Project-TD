using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemySMManager enemySMManager) : base(enemySMManager)
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
    public override void Update()
    {
        if (!EnemySMManager.Enemy.EnemyAttack.AttackReady) return;
        EnemySMManager.Enemy.EnemyAttackCollider.DetectEnenmy();
    }

  
}
