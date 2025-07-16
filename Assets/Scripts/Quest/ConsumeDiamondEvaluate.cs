using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeDiamondEvaluate : IQuestEvaluate
{
    private int TargetAmount;
    private int CurrentAmount;

    public event Action OnProgressChange;

    public ConsumeDiamondEvaluate(int targetAmount )
    {
        TargetAmount = targetAmount;
        CurrentAmount = 0;
    }

    public string GetProgress()
    {
        return CurrentAmount.ToString();
    }

    public float GetProgressPercent()
    {
        return CurrentAmount / TargetAmount;
    }

    public void GoalReset()
    {
        CurrentAmount = 0;
    }

    public void Initialized()
    {
        QuestEventHandler.OnConsumeDiamond += QuestEventHandler_OnConsumeDiamond;
    }

    private void QuestEventHandler_OnConsumeDiamond(int obj)
    {
        CurrentAmount += obj;
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
        QuestEventHandler.OnConsumeDiamond -= QuestEventHandler_OnConsumeDiamond;

    }


}
