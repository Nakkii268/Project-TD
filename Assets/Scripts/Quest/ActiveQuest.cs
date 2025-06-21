using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveQuest : MonoBehaviour
{
    public string QuestID;
    public string QuestTitle;
    public string QuestDescription;
    public string QuestType;
    public QuestState CurrentState;
    public IQuestEvaluate Evaluator;

    public bool IsCompleted => Evaluator != null && Evaluator.IsCompleted();
   
    
    public void Initialized()
    {
        Evaluator.Initialized();
    }
    public void LoadProgress(string saved)
    {
        Evaluator?.LoadProgress(saved);
    }
    public string GetProgress()
    {
        return Evaluator?.GetProgress();
    }
}
