using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyEvaluate : IQuestEvaluate
{
    private int TargetAmount;
    private int CurrentAmount;

    public KillEnemyEvaluate(int amount)
    {
        TargetAmount = amount;
    }

    public string GetProgress()
    {
        return CurrentAmount.ToString();
    }

    public void Initialized()
    {
        QuestEventHandler.OnStageClear += QuestEventHandler_OnStageClear;
    }

    private void QuestEventHandler_OnStageClear(MapSO obj)
    {
        CurrentAmount += obj.TotalEnemy;
    }

    public bool IsCompleted()
    {
        return CurrentAmount>=TargetAmount;
    }

 

    public void LoadProgress(string saved)
    {
        CurrentAmount = int.Parse(saved);
    }
}
