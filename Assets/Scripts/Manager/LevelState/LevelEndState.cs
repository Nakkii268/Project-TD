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
            ProgressCheck();
        }
        else if(LevelStateMachineManager.endState == EndState.NotComplete)
        {
            UIManager.Instance.OpenUI<WinUI>(false);
            ProgressCheck();

        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    
    private void ProgressCheck()
    {
        Debug.Log("++++++++++++++");
        if(GameManager.Instance._playerDataManager.PlayerDataSO.PlayerProgress.ChapterIndex == LevelStateMachineManager._levelManager.Map.ChapterIndex)
        {
           if( GameManager.Instance._playerDataManager.PlayerDataSO.PlayerProgress.Stage == LevelStateMachineManager._levelManager.Map.StageIndex){
                Debug.Log("-----------------");

                GameManager.Instance._playerDataManager.PlayerDataSO.UpdateProgress();
            }
        }
       ;
    }
    
}
