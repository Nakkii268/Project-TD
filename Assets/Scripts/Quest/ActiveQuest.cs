using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ActiveQuest 
{
    public string QuestID;
    public string QuestTitle;
    public string QuestDescription;
    public string QuestType;
    public QuestState CurrentState;
    public IQuestEvaluate Evaluator;
    public List<SaveItemData> Rewards;
    public bool IsCompleted => Evaluator != null && Evaluator.IsCompleted();
   
    
    public void Initialized()
    {
        Evaluator.Initialized();
    }
    public void LoadProgress(string saved)
    {
        Evaluator?.LoadProgress(saved);
    }
    public string GetProgress() //for showing 
    {
        return Evaluator?.GetProgress();
    }
    public void QuestCompleted()
    {
        CurrentState = QuestState.Completed;
        List<ItemsData> items = GetRewards();
        for (int i = 0; i < Rewards.Count; i++)
        {
            GameManager.Instance._playerDataManager.PlayerDataSO.AddItem(items[i].Item, items[i].Quantity);
        }
    }
    public List<ItemsData> GetRewards()
    {
        List<ItemsData> data = new List<ItemsData>();
        for (int i = 0; i < Rewards.Count; i++)
        {
            Item item = GameManager.Instance._resourceManager.GetItemById<Item>(Rewards[i].ItemId);
            data.Add(new ItemsData(item,Rewards[i].Quantity));
          
        }
        return data;
    }
}
