using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyQuestEvaluate : IQuestEvaluate
{
    public int TargetPoint;
    public int CurrentPoint;
    public event Action OnProgressChange;
    public DailyQuestEvaluate(int targetPoint)
    {
        TargetPoint = targetPoint;
        CurrentPoint = 0;
    }

    public string GetProgress()
    {
        return CurrentPoint.ToString();
    }

    public float GetProgressPercent()
    {
        return (float)CurrentPoint/TargetPoint;
    }

    public void Initialized()
    {
        QuestEventHandler.DailyPointAcquire += QuestEventHandler_DailyPointAcquire;
    }

    private void QuestEventHandler_DailyPointAcquire(int obj)
    {
        CurrentPoint += obj;
    }

    public bool IsCompleted()
    {
        return CurrentPoint>= TargetPoint;
    }

    public void LoadProgress(string saved)
    {
       CurrentPoint = int.Parse(saved);
    }

    public void UnsubEvent()
    {
        QuestEventHandler.DailyPointAcquire -= QuestEventHandler_DailyPointAcquire;

    }

    public void GoalReset()
    {
        CurrentPoint= 0;
    }
}
