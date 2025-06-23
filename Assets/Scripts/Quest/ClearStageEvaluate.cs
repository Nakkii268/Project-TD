using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStageEvaluate : IQuestEvaluate
{
    
    private string targetStage;
    private bool isCompleted= false;

    public event Action OnProgressChange;

    public ClearStageEvaluate(string tg)
    {
        targetStage = tg;
    }
    public string GetProgress()
    {
        if (isCompleted)
        {
            return "Completed";
        }
        return "Incomplete";
    }
    public float GetProgressPercent()
    {
        if (isCompleted)
        {
            return 1;
        }
        return 0;
    }

    public void Initialized( )
    {
        QuestEventHandler.OnStageClear += Context_OnStageClear;
    }

    private void Context_OnStageClear( MapSO e)
    {
        if (e.MapID == targetStage)
        {
            isCompleted = true;
            OnProgressChange?.Invoke();
        }
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }

    public void LoadProgress(string saved)
    {
        if(saved == "Completed")
        {
            isCompleted = true;

        }
        else
        {
            isCompleted = false;
        }
    }

    public void UnsubEvent()
    {
        QuestEventHandler.OnStageClear -= Context_OnStageClear;

    }
}
