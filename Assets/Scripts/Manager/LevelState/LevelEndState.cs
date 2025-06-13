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
            ItemDrop(LevelStateMachineManager._levelManager.Map);

        }
        else if(LevelStateMachineManager.endState == EndState.NotComplete)
        {
            UIManager.Instance.OpenUI<WinUI>(false);
            ProgressCheck();
            ItemDrop(LevelStateMachineManager._levelManager.Map);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
    
    private void ProgressCheck()
    {
        
        if(GameManager.Instance._playerDataManager.PlayerDataSO.PlayerProgress.ChapterIndex == LevelStateMachineManager._levelManager.Map.ChapterIndex)
        {
           if( GameManager.Instance._playerDataManager.PlayerDataSO.PlayerProgress.Stage == LevelStateMachineManager._levelManager.Map.StageIndex){
               

                GameManager.Instance._playerDataManager.PlayerDataSO.UpdateProgress();
            }
        }
       
    }
    private void ItemDrop(MapSO map)
    {
        for (int i = 0; i < map.DropItem.Count; i++)
        {
            GameManager.Instance._playerDataManager.PlayerDataSO.AddItem(map.DropItem[i].Item, map.DropItem[i].Quantity);
        }
        GameManager.Instance._playerDataManager.SaveItem();
        
    }
    
}
