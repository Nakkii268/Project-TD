using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisableState : EnemyState
{
    public EnemyDisableState(EnemySMManager enemySMManager) : base(enemySMManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        EnemySMManager._enemy.EnemyVisual.PlayDisableAnim();

    }
    public override void Exit() 
    { 
        base.Exit(); 
    }

    protected override void AddCallBack()
    {
        base.AddCallBack();
        EnemySMManager._enemy.StatusEffectHolder.OnEndDisable += StatusEffectHolder_OnEndDisable;
    }
    protected override void RemoveCallBack()
    {
        EnemySMManager._enemy.StatusEffectHolder.OnEndDisable -= StatusEffectHolder_OnEndDisable;

    }

    private void StatusEffectHolder_OnEndDisable(object sender, System.EventArgs e)
    {
        if(EnemySMManager._enemy.IsMoving  && !EnemySMManager._enemy.IsBlocked)
        {
            EnemySMManager.ChangeState(EnemySMManager.EnemyMovingState);
            return;
        }
        EnemySMManager.ChangeState(EnemySMManager.EnemyIdleState);

    }
}
