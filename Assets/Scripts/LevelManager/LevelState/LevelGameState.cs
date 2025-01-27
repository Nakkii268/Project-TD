using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameState : LevelState
{
    
    public LevelGameState(LevelStateMachineManager levelStateMachineManager) : base(levelStateMachineManager)
    {
    }
    public override void Enter()
    {
        base.Enter();
        LevelStateMachineManager.entryTime = Time.time;
        LevelStateMachineManager._levelManager.EnableComponent();
        
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    public override void Update()
    {
        base.Update();
    }
}
