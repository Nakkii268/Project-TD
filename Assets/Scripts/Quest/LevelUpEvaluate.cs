using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEvaluate : IQuestEvaluate
{
    private int TargetLevel;
    private int CurrentLevel;

    public event Action OnProgressChange;

    public LevelUpEvaluate(int tg) { 
        TargetLevel = tg;
        CurrentLevel = 0;
    }
    public string GetProgress()
    {
        return CurrentLevel.ToString();
    }
    public float GetProgressPercent()
    {
        return (float)CurrentLevel /TargetLevel;
    }
    public void Initialized()
    {
        QuestEventHandler.OnUnitLevelup += QuestEventHandler_OnUnitLevelup;
    }

    private void QuestEventHandler_OnUnitLevelup(int obj)
    {
        CurrentLevel = obj;
        OnProgressChange?.Invoke();

    }

    public bool IsCompleted()
    {
        return CurrentLevel >= TargetLevel;
    }

    public void LoadProgress(string saved)
    {
        CurrentLevel = int.Parse(saved);
    }

    public void UnsubEvent()
    {
        QuestEventHandler.OnUnitLevelup -= QuestEventHandler_OnUnitLevelup;

    }
}
