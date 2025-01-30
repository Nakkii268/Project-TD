using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyState :  IState
{

    public EnemySMManager EnemySMManager;
    public EnemyState previousState;
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

    private void Enemy_OnEnemyDead(object sender, EnemyDeadArg e)
    {
        EnemySMManager.ChangeState(EnemySMManager.EnemyDeadState);

    }
    protected void Move()
    {

        Vector3 target;
            if (EnemySMManager.Enemy.Path.Length == 0) return;
            target = new Vector3(EnemySMManager.Enemy.Path[EnemySMManager.Enemy.pathIndex].x, EnemySMManager.Enemy.Path[EnemySMManager.Enemy.pathIndex].y, EnemySMManager.Enemy.transform.position.z);
            EnemySMManager.Enemy.transform.position = Vector3.MoveTowards(EnemySMManager.Enemy.transform.position, target, EnemySMManager.Enemy.speed * Time.deltaTime);
            CheckNextIndex();
            //move
            if (Vector3.Distance(new Vector3(EnemySMManager.Enemy.transform.position.x, EnemySMManager.Enemy.transform.position.y, 0), new Vector3(target.x, target.y, 0)) < .3f)
            {
                if (EnemySMManager.Enemy.pathIndex >= EnemySMManager.Enemy.Path.Length - 1) return;
                EnemySMManager.Enemy.pathIndex++;
                target = EnemySMManager.Enemy.Path[EnemySMManager.Enemy.pathIndex];

            }
            Vector2 dir = (Vector2)target - new Vector2(EnemySMManager.Enemy.transform.position.x, EnemySMManager.Enemy.transform.position.y);
        
    }
    private void CheckNextIndex()
    {
        if (EnemySMManager.Enemy.Path[EnemySMManager.Enemy.pathIndex].x != -99)
        {
            return;
        }
        EnemySMManager.ChangeState(EnemySMManager.EnemyIdleState);

    }
    protected float GetWaitTime()
    {
        return EnemySMManager.Enemy.Path[EnemySMManager.Enemy.pathIndex].y;
    }
}
