using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : UICanvas
{
   
    [SerializeField] private Transform Container;
    [SerializeField] private QuestSingle prefab;
    [SerializeField] private QuestManager questManger;
    //navigate btn
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;

    //
    [SerializeField] private Button DailyBtn;
    [SerializeField] private Button WeeklyBtn;
    [SerializeField] private Button AchieveBtn;
    [SerializeField] private List<Transform> CategoryActives;
    [SerializeField] private int CurrentCategory = 0;

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
        DailyBtn.onClick.AddListener(() =>
            {
                CategoryBtnHandle(0);
                CategorySelect(0);
            });
        WeeklyBtn.onClick.AddListener(() =>
            {
                CategoryBtnHandle(1);
                CategorySelect(1);
            });
        AchieveBtn.onClick.AddListener(() =>
            {
                CategoryBtnHandle(2);
                CategorySelect(2);
            });

        
    }
    public override void SetUp()
    {
        questManger = GameManager.Instance._questManager;
        CategorySelect(0);
    }

    private void SetUpUI()
    {
        CategorySelect(CurrentCategory);

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
    private void CategoryActive(int index)
    {
        for(int i = 0;i< CategoryActives.Count; i++)
        {
            CategoryActives[i].gameObject.SetActive(false);
            if(i == index)
            {
                CategoryActives[i].gameObject.SetActive(true);

            }
        }
    }
    private void CategoryBtnHandle(int index)
    {
        DailyBtn.interactable = true;
        WeeklyBtn.interactable = true;
        AchieveBtn.interactable = true;
        switch (index)
        {
            case 0: DailyBtn.interactable = false; break;
            case 1: WeeklyBtn.interactable = false; break;
            case 2: AchieveBtn.interactable = false; break;
            default: break;
        }
    }
    private void CategorySelect(int index)
    {
        CategoryActive(index);
        ClearChild();
        CurrentCategory=index;
        List<ActiveQuest> list = questManger.GetQuests(index);
        for (int i = 0; i < list.Count; i++)
        {
            QuestSingle single = Instantiate(prefab, Container);
            single.Inititalized(list[i]);
            single.OnClaim += Single_OnClaim;
        }
    }
}
