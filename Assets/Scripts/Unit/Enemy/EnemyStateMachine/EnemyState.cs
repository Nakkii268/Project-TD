using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState :  IState
{

    public EnemySMManager EnemySMManager;
    public EnemyState(EnemySMManager enemySMManager)
    {
           EnemySMManager = enemySMManager; 
    }
    public virtual void Enter()
    {
        AddCallBack();
    }

    public virtual void Exit()
    {
        RemoveCallBack();
    }
    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
       
    }

    public virtual void OnAnimationEnterEvent()
    {
    }

    public virtual void OnAnimationExitEvent()
    {
    }

    public virtual void OnAnimationTransitionEvent()
    {
    }
    protected virtual void AddCallBack()
    {
        EnemySMManager.Enemy.OnEnemyDead += Enemy_OnEnemyDead;
        EnemySMManager.Enemy.OnGetHit += Enemy_OnGetHit;
        EnemySMManager.Enemy.StatusEffectHolder.OnGetDisable += StatusEffectHolder_OnGetDisable;
    }

    

    protected virtual void RemoveCallBack()
    {
        EnemySMManager.Enemy.OnEnemyDead -= Enemy_OnEnemyDead;
        EnemySMManager.Enemy.OnGetHit -= Enemy_OnGetHit;
    }

    private void StatusEffectHolder_OnGetDisable(object sender, System.EventArgs e)
    {
        EnemySMManager.ChangeState(EnemySMManager.EnemyDisableState);
    }
    private void Enemy_OnGetHit(object sender, System.EventArgs e)
    {
        EnemySMManager.ChangeState(EnemySMManager.EnemyGetHitState);
    }

    private void Enemy_OnEnemyDead(object sender, GameObject e)
    {
        EnemySMManager.ChangeState(EnemySMManager.EnemyDeadState);

    }
    protected void Move()
    {
        EnemySMManager.Enemy.GetNextTarget();
    }
}
