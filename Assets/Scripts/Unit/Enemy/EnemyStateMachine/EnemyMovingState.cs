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
        EnemySMManager._enemy.IsMoving = true;
        EnemySMManager._enemy.EnemyVisual.PlayMoveAnim();

    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        Move();
    }
   
    
}
