using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : UICanvas
{
    [SerializeField] private QuestSingle single;
    [SerializeField] private Transform Container;
    [SerializeField] private QuestSingle prefab;
    [SerializeField] private QuestManager questManger;
    //navigate btn
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;

    private void Start()
    {
       
        BackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close<QuestUI>(0);
        });
        HomeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.CloseToHome();
        });
    }
    public override void SetUp()
    {
        questManger = GameManager.Instance._questManager;
        for(int i=0; i< questManger.activeQuests.Count;i++)
        {
            QuestSingle single = Instantiate(prefab, Container);
            single.Inititalized(questManger.activeQuests[i]);
        }
    }
}
