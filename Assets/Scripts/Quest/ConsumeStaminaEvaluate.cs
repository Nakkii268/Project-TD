using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeStaminaEvaluate : IQuestEvaluate
{
    private int TargetAmount;
    private int CurrentAmount;
    public ConsumeStaminaEvaluate(int targetAmount)
    {
        TargetAmount = targetAmount;
        CurrentAmount= 0;   
    }
    public event Action OnProgressChange;

    public string GetProgress()
    {
        return CurrentAmount.ToString();
    }

    public float GetProgressPercent()
    {
        return CurrentAmount/TargetAmount;
    }

    public void GoalReset()
    {
        CurrentAmount = 0;
    }

    public void Initialized()
    {
        QuestEventHandler.OnConsumeStamina += QuestEventHandler_OnConsumeStamina;
    }

    private void QuestEventHandler_OnConsumeStamina(int obj)
    {
        CurrentAmount += obj;   
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
        QuestEventHandler.OnConsumeStamina -= QuestEventHandler_OnConsumeStamina;

    }


}
