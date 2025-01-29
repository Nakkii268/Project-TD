using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPrepareState : LevelState
{
    private float waitTime=0;
    public LevelPrepareState(LevelStateMachineManager levelStateMachineManager) : base(levelStateMachineManager)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
        if(waitTime >= 3)
        {
            LevelStateMachineManager.ChangeState(LevelStateMachineManager.LevelGameState);

        }
        waitTime += Time.deltaTime;
    }
    protected override void AddCallBack()
    {
    }
    protected override void RemoveCallBack()
    {
    }
}
