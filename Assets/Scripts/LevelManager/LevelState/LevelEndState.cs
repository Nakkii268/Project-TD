using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndState : LevelState
{
    public LevelEndState(LevelStateMachineManager levelStateMachineManager) : base(levelStateMachineManager)
    {
    }
    public override void Enter()
    {
        base.Enter();
        LevelStateMachineManager._levelManager.EndUI.ActiveUI(endState);
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
        //after few second, load to main sceen
    }
    
}
