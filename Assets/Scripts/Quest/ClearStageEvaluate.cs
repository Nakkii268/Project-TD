using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStageEvaluate : IQuestEvaluate
{
    
    private string targetStage;
    private int targetAmount;
    private int CurrentAmount;
   

    public event Action OnProgressChange;

    public ClearStageEvaluate( int ta, string tg=null)
    {
        targetStage = tg;
        targetAmount = ta;
        CurrentAmount= 0;
    }
    public string GetProgress()
    {
        return CurrentAmount.ToString();
    }
    public float GetProgressPercent()
    {
       
        return (float)CurrentAmount/targetAmount;
    }

    public void Initialized( )
    {
        QuestEventHandler.OnStageClear += Context_OnStageClear;
    }

    private void Context_OnStageClear(MapSO e)
    {
        if (targetStage == null||e.MapID == targetStage )
        {
            CurrentAmount += 1;
            OnProgressChange?.Invoke();
        }
    }

    public bool IsCompleted()
    {
        return CurrentAmount>=targetAmount;
    }

    public void LoadProgress(string saved)
    {
        
            CurrentAmount = int.Parse(saved);

      
    }

    public void UnsubEvent()
    {
        QuestEventHandler.OnStageClear -= Context_OnStageClear;

    }

    public void GoalReset()
    {
       CurrentAmount=0;
    }
}
