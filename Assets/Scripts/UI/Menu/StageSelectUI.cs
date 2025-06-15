using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class StageSelectUI : UICanvas
{
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
   
    [SerializeField] public StageUI StageList;

    [SerializeField] private List<Progress> _playerProgress;
   
    //chapter
    [SerializeField] private int CurrentSelectChap;//index
    [SerializeField] private Button PrevChapBtn;
    [SerializeField] private Button NextChapBtn;
    [SerializeField] private TextMeshProUGUI ChapterTxt;
    [SerializeField] public Dictionary<int,ChapterSO> Chapters=new Dictionary<int,ChapterSO>();
    // Start is called before the first frame update
    void Start()
    {
        BackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close<StageSelectUI>(0);
        });
        HomeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ToHomeMenu();
        });
        PrevChapBtn.onClick.AddListener(() =>
        {
            CurrentSelectChap--;
            UpdateChapterTxt();
            LoadStageList(CurrentSelectChap);
            ButtonUpdate();
        });
        NextChapBtn.onClick.AddListener(() =>
        {
            if (_playerProgress[CurrentSelectChap].StageList.Count == Chapters[CurrentSelectChap].StageQuantity)
            {
                CurrentSelectChap++;
                UpdateChapterTxt();
                LoadStageList(CurrentSelectChap);
                ButtonUpdate();
            }
        });

    }
    public override void SetUp()
    {
        Initialized();
    }
    private void Initialized()
    {
        _playerProgress = GameManager.Instance._playerDataManager.PlayerDataSO.PlayerProgress;
        CurrentSelectChap =_playerProgress.Count -1 ;
        for(int i = 0; i <= _playerProgress.Count+1; i++)
        {
           Chapters.Add(i, GameManager.Instance._resourceManager.GetChapterByIndex<ChapterSO>(i));
        }
        LoadStageList(CurrentSelectChap);

       
    }

   

    public void LoadStageList(int cIndex)
    {
        Addressables.LoadAssetAsync<GameObject>(Chapters[cIndex].StageUIPath).Completed += ((handler) =>
        {
            if (handler.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                GameObject newStageList = Instantiate(handler.Result, transform);
                newStageList.transform.SetAsFirstSibling();
                if (StageList != null)
                {
                    Destroy(StageList.gameObject);
                }
                StageUI stageUI = newStageList.GetComponent<StageUI>();
                StageList = stageUI;
                stageUI.Initialized();


            }
        });
        
    }
   

    

    private void UpdateChapterTxt()
    {
        ChapterTxt.text = "Chapter. " + (CurrentSelectChap + 1).ToString();
    }
    private void ButtonUpdate()
    {
        if(CurrentSelectChap == _playerProgress.Count-1)
        {
            NextChapBtn.interactable = false;
        }
        else
        {
            NextChapBtn.interactable = true;

        }
        if (CurrentSelectChap == 0)
        {
            PrevChapBtn.interactable = false;
        }
        else
        {
            PrevChapBtn.interactable = true;

        }
    }
}
[Serializable]
public class Progress
{
    public int ChapterIndex;
    
    public List<StageData> StageList;
    public Progress(int chapterIndex)
    {
        ChapterIndex = chapterIndex;
        
        StageList = new List<StageData>();
    }
    public Progress() {
        ChapterIndex = 0;
        StageList = new List<StageData>() ;
    }
}
[Serializable]
public class StageData
{
    
    public int Stage;
    public int Rating;
    public StageData(int stage, int rating)
    {
       
        Stage = stage;
        Rating = rating;
    }
    
}
