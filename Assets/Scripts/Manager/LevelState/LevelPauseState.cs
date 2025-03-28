using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPauseState : LevelState
{
    public LevelPauseState(LevelStateMachineManager levelStateMachineManager) : base(levelStateMachineManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0f;
    }
    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1f;

    }
    public override void FixedUpdate()
    {
    }
    public override void Update()
    {
    }
    protected override void AddCallBack()
    {
        LevelStateMachineManager._levelManager.OnGameUnPause += LevelManager_OnGameUnPause;
    }

    
    protected override void RemoveCallBack()
    {
        LevelStateMachineManager._levelManager.OnGameUnPause -= LevelManager_OnGameUnPause;

    }
    private void LevelManager_OnGameUnPause(object sender, System.EventArgs e)
    {
        LevelStateMachineManager.ChangeState(LevelStateMachineManager.LevelGameState);
    }

}
