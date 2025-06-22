using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Dictionary<string, ActiveQuest> QuestList = new Dictionary<string, ActiveQuest>();
    public List<ActiveQuest> activeQuests = new List<ActiveQuest>();
    public List<ActiveQuest> CurrentactiveQuests = new List<ActiveQuest>();
    private void Start()
    {
        string path =Path.Combine( Application.persistentDataPath , "Quest.json");
        string json  = File.ReadAllText(path);
        List<QuestConfig> configs = JsonUtility.FromJson<QuestListWrapper>(json).list;
        LoadConfig(configs);
        CurrentactiveQuests = GetActiveQuest();
    }
    public void LoadConfig(List<QuestConfig> configs)
    {
        Debug.Log(configs.Count + "quest");
        foreach (var config in configs)
        {
            var evaluator = GoalFactory.Create(config.GoalConfig);
            var quest = new ActiveQuest()
            {
                QuestID = config.QuestID,
                QuestTitle = config.QuestTitle,
                QuestDescription = config.QuestDescription,
                CurrentState = config.CurrentState,
                QuestType = config.QuestType,
                Evaluator = evaluator,
                Rewards = config.Rewards
            };
            quest.Initialized();
            QuestList[quest.QuestID] = quest;
            activeQuests.Add(quest);
        }
    }
    public List<ActiveQuest> GetActiveQuest()
    {
        List<ActiveQuest> quests = new List<ActiveQuest>();
        for(int i = 0; i < activeQuests.Count; i++)
        {
            if (activeQuests[i].CurrentState == QuestState.OnGoing)
            {
                quests.Add(activeQuests[i]);
            }
        }
        return quests;
    }

}
