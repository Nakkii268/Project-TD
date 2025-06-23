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
    public string FollowupQuest; //next quest that unlock by completed this quest
    public List<SaveItemData> Rewards;
    public GoalConfig GoalConfig; //for savedataback
    public bool IsCompleted => Evaluator != null && Evaluator.IsCompleted();
   public event Action<string> OnCompleted;
    
    public void Initialized()
    {
        if(CurrentState == QuestState.OnGoing)
        {
            Evaluator.Initialized();
            Evaluator.OnProgressChange += Evaluator_OnProgressChange;

        }
    }

    private void Evaluator_OnProgressChange()
    {
        GameManager.Instance._playerDataManager.PlayerDataSO.UpdateQuestProgress(QuestID, GetProgress());

    }

    public void LoadProgress(string saved)
    {
        Evaluator?.LoadProgress(saved);
    }
    public string GetProgress() //for showing 
    {
        return Evaluator?.GetProgress();
    }
    public float GetProgressPercent()
    {
        return Evaluator.GetProgressPercent();
    }
    public void QuestCompleted()
    {
        CurrentState = QuestState.Completed;
        Evaluator?.UnsubEvent();
        OnCompleted?.Invoke(FollowupQuest);
        List<ItemsData> items = GetRewards();
        for (int i = 0; i < Rewards.Count; i++)
        {
            GameManager.Instance._playerDataManager.PlayerDataSO.AddItem(items[i].Item, items[i].Quantity);
        }
        GameManager.Instance._playerDataManager.PlayerDataSO.RemoveQuest(QuestID);

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
    public void QuestAcquire()
    {
        CurrentState = QuestState.OnGoing;
        GameManager.Instance._playerDataManager.PlayerDataSO.AddQuest(QuestID,GetProgress());

        Initialized();
    }
}
