using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour, IState
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
    }
    protected virtual void RemoveCallBack()
    {
        EnemySMManager.Enemy.OnEnemyDead -= Enemy_OnEnemyDead;
        EnemySMManager.Enemy.OnGetHit -= Enemy_OnGetHit;
    }

    private void Enemy_OnGetHit(object sender, System.EventArgs e)
    {
        EnemySMManager.ChangeState(EnemySMManager.EnemyGetHitState);
    }

    private void Enemy_OnEnemyDead(object sender, GameObject e)
    {
        EnemySMManager.ChangeState(EnemySMManager.EnemyDeadState);

    }
}
