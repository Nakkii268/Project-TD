using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySMManager :StateMachineManager
{
    public Enemy Enemy{  get;  }
    public EnemyIdleState EnemyIdleState { get; }
    public EnemyAttackState EnemyAttackState { get; }
    public EnemyGetHitState EnemyGetHitState { get; }
    public EnemyDeadState EnemyDeadState { get; }
    public EnemyMovingState EnemyMovingState { get; }

    public EnemySMManager(Enemy enemy)
    {
        Enemy = enemy;
        EnemyIdleState = new EnemyIdleState(this);
        EnemyAttackState = new EnemyAttackState(this);
        EnemyGetHitState = new EnemyGetHitState(this);
        EnemyDeadState = new EnemyDeadState(this);
        EnemyMovingState = new EnemyMovingState(this);
    }


}
