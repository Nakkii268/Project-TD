using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStageEvaluate : IQuestEvaluate
{
    private string targetStage;
    private bool isCompleted= false;

    public ClearStageEvaluate(string tg)
    {
        targetStage = tg;
    }
    public string GetProgress()
    {
        return isCompleted.ToString();
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
        }
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }

    public void LoadProgress(string saved)
    {
        isCompleted= bool.Parse(saved);
    }
}
