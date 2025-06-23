using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyEvaluate : IQuestEvaluate
{
    private int TargetAmount;
    private int CurrentAmount;

    public event Action OnProgressChange;

    public KillEnemyEvaluate(int amount)
    {
        TargetAmount = amount;
        CurrentAmount = 0;
    }

    public string GetProgress()
    {
        return CurrentAmount.ToString();
    }
    public float GetProgressPercent()
    {
        return (float)CurrentAmount/TargetAmount;
    }
    public void Initialized()
    {
        QuestEventHandler.OnStageClear += QuestEventHandler_OnStageClear;
    }

    private void QuestEventHandler_OnStageClear(MapSO obj)
    {
        CurrentAmount += obj.TotalEnemy;
        OnProgressChange?.Invoke();

    }

    public bool IsCompleted()
    {
        return CurrentAmount>=TargetAmount;
    }

 

    public void LoadProgress(string saved)
    {
        CurrentAmount = int.Parse(saved);
    }

    public void UnsubEvent()
    {
        QuestEventHandler.OnStageClear -= QuestEventHandler_OnStageClear;

    }
}
