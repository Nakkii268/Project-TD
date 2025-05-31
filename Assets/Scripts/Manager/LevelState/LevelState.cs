using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelState : IState
{
    protected LevelStateMachineManager LevelStateMachineManager;
    
    public LevelState (LevelStateMachineManager levelStateMachineManager)
    {
        LevelStateMachineManager = levelStateMachineManager;     
    }
    public virtual void Enter()
    {
        AddCallBack();
    }

    public virtual void Exit()
    {
        RemoveCallBack();
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

    public virtual void Update()
    {
    }
    protected virtual void AddCallBack()
    {
        
        LevelStateMachineManager._levelManager.OnGameEnd += LevelManager_OnGameEnd;
    }

   

    protected virtual void RemoveCallBack()
    {
       
        LevelStateMachineManager._levelManager.OnGameEnd -= LevelManager_OnGameEnd;

    }

   
    private void LevelManager_OnGameEnd(object sender, EndState e)
    {
        LevelStateMachineManager.endState = e;
        LevelStateMachineManager.ChangeState(LevelStateMachineManager.LevelEndState);
    }
}
