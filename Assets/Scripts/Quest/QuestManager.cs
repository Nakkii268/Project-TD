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
    [SerializeField] private PlayerDataManager playerDataManager;
    private void Start()
    {
        string path =Path.Combine( Application.persistentDataPath , "Quest.json");
        string json  = File.ReadAllText(path);
        List<QuestConfig> configs = JsonUtility.FromJson<QuestListWrapper>(json).list;
        LoadConfig(configs);
        CurrentactiveQuests = GetActiveQuest();
        playerDataManager = GameManager.Instance._playerDataManager;
        playerDataManager.OnPlayerDataLoaded += PlayerDataManager_OnPlayerDataLoaded;
    }
    private void SaveQuestState()
    {
        string path = Path.Combine(Application.persistentDataPath, "Quest.json");

        QuestListWrapper newWrapper = new();
        foreach (var quest in QuestList)
        {
            QuestConfig config = new QuestConfig()
            {
                QuestID = quest.Value.QuestID,
                QuestTitle = quest.Value.QuestTitle,
                QuestDescription = quest.Value.QuestDescription,
                QuestType = quest.Value.QuestType,
                CurrentState = quest.Value.CurrentState,
                FollowupQuest = quest.Value.FollowupQuest,
                Rewards = quest.Value.Rewards,
                GoalConfig = quest.Value.GoalConfig
            };
            newWrapper.list.Add(config);
        }
        string json = JsonUtility.ToJson(newWrapper);
        File.WriteAllText(path, json);
    }
    private void PlayerDataManager_OnPlayerDataLoaded()
    {
        LoadQuestProgress();
    }

    public void LoadConfig(List<QuestConfig> configs)
    {
       
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
                FollowupQuest = config.FollowupQuest,
                Rewards = config.Rewards,
                GoalConfig= config.GoalConfig
            };

            quest.Initialized();
            if(quest.CurrentState == QuestState.OnGoing)
            {
                quest.OnCompleted += Quest_OnCompleted; ;
            }
            QuestList[quest.QuestID] = quest;
            activeQuests.Add(quest);
        }
       

    }

    private void Quest_OnCompleted(string obj)
    {
        if(obj =="") return;
        QuestList[obj].QuestAcquire();
        SaveQuestState();
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
    public void LoadQuestProgress()
    {
        for (int i = 0; i < playerDataManager.PlayerDataSO.QuestData.Count; i++)
        {
            if (QuestList.ContainsKey(playerDataManager.PlayerDataSO.QuestData[i].QuestID))
            {
                QuestList[playerDataManager.PlayerDataSO.QuestData[i].QuestID].LoadProgress(playerDataManager.PlayerDataSO.QuestData[i].QuestProgress);
            }
        }
    }

}
