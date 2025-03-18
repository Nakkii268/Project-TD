using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    private float timeCounter=0;
    public EnemyIdleState(EnemySMManager enemySMManager) : base(enemySMManager)
    {
    }


    public override void Enter()
    {
        base.Enter();
        EnemySMManager._enemy.EnemyVisual.PlayIdleAnim();

    }

    public override void Exit()
    {

        timeCounter = 0;
        base.Exit();
    }
    public override void Update()
    {
        if(timeCounter > GetWaitTime())
        {
            EnemySMManager._enemy.pathIndex ++;
            EnemySMManager.ChangeState(EnemySMManager.EnemyMovingState); 
        }
        timeCounter += Time.deltaTime;
        if (EnemySMManager._enemy.EnemyAttack.CanPerformAttack())
        {
            EnemySMManager.ChangeState(EnemySMManager.EnemyAttackState);
        }
    }
    
  
}
