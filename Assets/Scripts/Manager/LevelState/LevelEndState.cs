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
            UIManager.Instance.OpenUI<WinUI>(new MapData(LevelStateMachineManager._levelManager.Map,(int)EndState.Successed));
            GameManager.Instance._playerDataManager.PlayerDataSO.UpdateProgress(LevelStateMachineManager._levelManager.Map, (int)LevelStateMachineManager.endState);
       
            ItemDrop(LevelStateMachineManager._levelManager.Map);
            QuestEventHandler.StageClear(LevelStateMachineManager._levelManager.Map);

        }
        else if(LevelStateMachineManager.endState == EndState.NotComplete)
        {
            UIManager.Instance.OpenUI<WinUI>(new MapData(LevelStateMachineManager._levelManager.Map, (int)EndState.NotComplete));
            GameManager.Instance._playerDataManager.PlayerDataSO.UpdateProgress(LevelStateMachineManager._levelManager.Map, (int)LevelStateMachineManager.endState);
           
            
            ItemDrop(LevelStateMachineManager._levelManager.Map);
            QuestEventHandler.StageClear(LevelStateMachineManager._levelManager.Map);

        }
    }
    public override void Exit()
    {
        base.Exit();
   

    }


    private void ItemDrop(MapSO map)
    {
        for (int i = 0; i < map.DropItem.Count; i++)
        {
            GameManager.Instance._playerDataManager.PlayerDataSO.AddItem(map.DropItem[i].Item, map.DropItem[i].Quantity);
        }
       
        
    }
    
}
