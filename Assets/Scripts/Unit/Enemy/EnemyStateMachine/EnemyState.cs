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
        
        EnemySMManager._enemy.OnEnemyDead += Enemy_OnEnemyDead;
        EnemySMManager._enemy.OnGetHit += Enemy_OnGetHit;
        EnemySMManager._enemy.StatusEffectHolder.OnGetDisable += StatusEffectHolder_OnGetDisable;
    }

   
    protected virtual void RemoveCallBack()
    {
        

        EnemySMManager._enemy.OnEnemyDead -= Enemy_OnEnemyDead;
        EnemySMManager._enemy.OnGetHit -= Enemy_OnGetHit;
        EnemySMManager._enemy.StatusEffectHolder.OnGetDisable -= StatusEffectHolder_OnGetDisable;

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
            if (EnemySMManager._enemy.Path.Length == 0) return;
            target = new Vector3(EnemySMManager._enemy.Path[EnemySMManager._enemy.pathIndex].x, EnemySMManager._enemy.Path[EnemySMManager._enemy.pathIndex].y, EnemySMManager._enemy.transform.position.z);
            EnemySMManager._enemy.transform.position = Vector3.MoveTowards(EnemySMManager._enemy.transform.position, target, EnemySMManager._enemy.Stat.Speed.Value * Time.deltaTime);
            CheckNextIndex();
            //move
            if (Vector3.Distance(new Vector3(EnemySMManager._enemy.transform.position.x, EnemySMManager._enemy.transform.position.y, 0), new Vector3(target.x, target.y, 0)) < .1f)
            {
                if (EnemySMManager._enemy.pathIndex >= EnemySMManager._enemy.Path.Length - 1) return;
                EnemySMManager._enemy.pathIndex++;
                target = EnemySMManager._enemy.Path[EnemySMManager._enemy.pathIndex];

            }
        EnemySMManager._enemy.EnemyVisual.SetSortingOrder(EnemySMManager._enemy.transform.position.y);
        //  Vector2 dir = (Vector2)target - new Vector2(EnemySMManager._enemy.transform.position.x, EnemySMManager._enemy.transform.position.y);

    }
    private void CheckNextIndex()
    {
        if (EnemySMManager._enemy.Path[EnemySMManager._enemy.pathIndex].x != -99)
        {
            return;
        }
        EnemySMManager.ChangeState(EnemySMManager.EnemyIdleState);

    }
    protected float GetWaitTime()
    {
        return EnemySMManager._enemy.Path[EnemySMManager._enemy.pathIndex].y;
    }
}
