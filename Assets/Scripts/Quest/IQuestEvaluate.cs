using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestEvaluate 
{
    void Initialized();
    bool IsCompleted();
    string GetProgress();
    void LoadProgress(string saved);

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
    public Dictionary<string, string> Goals;
    public string Description;
}