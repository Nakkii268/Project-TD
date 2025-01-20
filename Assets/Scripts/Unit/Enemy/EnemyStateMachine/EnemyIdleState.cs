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
            EnemySMManager.Enemy.pathIndex ++;
            EnemySMManager.ChangeState(EnemySMManager.EnemyMovingState); 
        }
        timeCounter += Time.deltaTime;
        if (!EnemySMManager.Enemy.EnemyAttack.AttackReady) return;
        EnemySMManager.Enemy.EnemyAttackCollider.DetectEnenmy();
    }
    
  
}
