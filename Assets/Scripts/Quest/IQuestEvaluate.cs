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
    void GoalReset();
}
[Serializable]
public class QuestConfig 
{
    public string QuestID;
    public string QuestTitle;
    public string QuestDescription;
    public string QuestType;
    public QuestState CurrentState;
    public string FollowupQuest;
    public GoalConfig GoalConfig; 
    public List<SaveItemData> Rewards;
  
    public int Point; //active point with daily and weekly quest, or point for other milestone quest
}
[Serializable]
public class QuestListWrapper
{
    public  List<QuestConfig> list; 
    public QuestListWrapper() { 
        list = new List<QuestConfig>();
    }
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
    public List<string> Goals; // first element is target amount, second is target map, unit if had
    public string Description;
}