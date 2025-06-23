using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestEvaluate 
{
    event Action OnProgressChange;
    void Initialized();
    bool IsCompleted();
    string GetProgress();
    float GetProgressPercent();
    void LoadProgress(string saved);
    void UnsubEvent();

}
[Serializable]
public class QuestConfig 
{
    public string QuestID;
    public string QuestTitle;
    public string QuestDescription;
    public string QuestType;
    public QuestState CurrentState;
    public GoalConfig GoalConfig; 
    public List<SaveItemData> Rewards;
}
[Serializable]
public class QuestListWrapper
{
    public  List<QuestConfig> list; 
}
public enum QuestState
{
    Locked,
    OnGoing,
    Completed
}
[Serializable]
public class GoalConfig
{
    public string GoalType;
    public string Goals;
    public string Description;
}