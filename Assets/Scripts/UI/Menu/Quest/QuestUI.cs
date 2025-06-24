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
        SetUpUI();
    }

    private void SetUpUI()
    {
        ClearChild();
        List<ActiveQuest> list = questManger.GetActiveQuest();
        for (int i = 0; i < list.Count; i++)
        {
            QuestSingle single = Instantiate(prefab, Container);
            single.Inititalized(list[i]);
            single.OnClaim += Single_OnClaim;
        }
    }

    private void Single_OnClaim()
    {
        SetUpUI();
    }
    private void ClearChild()
    {
        for(int i = 0; i < Container.childCount; i++)
        {
            Destroy(Container.GetChild(i).gameObject);
        }
    }

    
}
