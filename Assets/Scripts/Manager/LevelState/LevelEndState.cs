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
        MapSO map = LevelStateMachineManager._levelManager.Map;
        bool FirstTime = GameManager.Instance._playerDataManager.PlayerDataSO.IsFirstTimePassed(map, (int)LevelStateMachineManager.endState);
        if (LevelStateMachineManager.endState == EndState.Failed)
        {
            UIManager.Instance.OpenUI<LoseUI>();
        }else if(LevelStateMachineManager.endState == EndState.Successed)
        {
            UIManager.Instance.OpenUI<WinUI>(new MapData(map, (int)EndState.Successed,FirstTime));
            ItemDrop(map, (int)LevelStateMachineManager.endState,FirstTime);
            GameManager.Instance._playerDataManager.PlayerDataSO.UpdateProgress(map, (int)LevelStateMachineManager.endState);
         
            QuestEventHandler.StageClear(map);
            GameManager.Instance._staminaManager.StaminaConsumed(map.StaminaCost);


        }
        else if(LevelStateMachineManager.endState == EndState.NotComplete)
        {
            UIManager.Instance.OpenUI<WinUI>(new MapData(map, (int)EndState.NotComplete));
            ItemDrop(map, (int)LevelStateMachineManager.endState, FirstTime);//check drop before update progress
            GameManager.Instance._playerDataManager.PlayerDataSO.UpdateProgress(map, (int)LevelStateMachineManager.endState);
            QuestEventHandler.StageClear(map);
            GameManager.Instance._staminaManager.StaminaConsumed(map.StaminaCost);

        }
    }
    public override void Exit()
    {
        base.Exit();
   

    }


    private void ItemDrop(MapSO map,int rating,bool firstTime)
    {
        Debug.Log(rating);
        PlayerDataSO playerSO = GameManager.Instance._playerDataManager.PlayerDataSO;
        for (int i = 0; i < map.DropItem.Count; i++)
        {
            playerSO.AddItem(map.DropItem[i].Item, map.DropItem[i].Quantity);
            Debug.Log("add");
        }
        if (firstTime)
        {
            playerSO.AddItem(map.FirstTimeClear.Item, map.FirstTimeClear.Quantity);
            Debug.Log("diamond");
        }
    }
    
}
