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
        
        if(LevelStateMachineManager.endState == EndState.Failed)
        {
            UIManager.Instance.OpenUI<LoseUI>();
        }else if(LevelStateMachineManager.endState == EndState.Successed)
        {
            UIManager.Instance.OpenUI<WinUI>(true);
        }else if(LevelStateMachineManager.endState == EndState.NotComplete)
        {
            UIManager.Instance.OpenUI<WinUI>(false);
        }
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
        if (Input.GetKeyDown(KeyCode.Space)) {
            GameManager.Instance._sceneLoader.LoadMenu();
        }
    }
    
}
