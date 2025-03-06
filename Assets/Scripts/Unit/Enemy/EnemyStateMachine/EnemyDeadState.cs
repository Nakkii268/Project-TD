using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(EnemySMManager enemySMManager) : base(enemySMManager)
    {
    }


    public override void Enter()
    {
        base.Enter();
        EnemySMManager._enemy.EnemyVisual.PlayDeadAnim();

    }

    public override void Exit()
    {
        base.Exit();
    }
   
   
    public override void OnAnimationExitEvent()
    {
        EnemySMManager._enemy.Dead();
    }
   
    
}
