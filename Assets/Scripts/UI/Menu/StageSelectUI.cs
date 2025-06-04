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
   
    [SerializeField] public GameObject StageList;

    [SerializeField] private Progress _playerProgress;
    [SerializeField] private Progress _playerLastStage;
    //chapter
    [SerializeField] private int CurrentSelectChap;//index
    [SerializeField] private Button PrevChapBtn;
    [SerializeField] private Button NextChapBtn;
    [SerializeField] private TextMeshProUGUI ChapterTxt;
    [SerializeField] private Dictionary<int,ChapterSO> Chapters=new Dictionary<int,ChapterSO>();
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
            CurrentSelectChap++;
            UpdateChapterTxt();
            LoadStageList(CurrentSelectChap);
            ButtonUpdate();

        });

    }
    public override void SetUp()
    {
        Initialized();
    }
    private void Initialized()
    {
        _playerProgress = GameManager.Instance._playerDataManager.PlayerDataSO.PlayerProgress;
        CurrentSelectChap = _playerProgress.ChapterIndex;
        for(int i = 0; i <= _playerProgress.ChapterIndex; i++)
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
                Destroy(StageList);
                StageList = newStageList;
                Debug.Log(StageList.name);
                DisableLockedStage(cIndex);


            }
        });
        
    }
   

    public void DisableLockedStage(int selectedChapter)
    {
        
        if (selectedChapter < _playerProgress.ChapterIndex) return;
        

        for (int i = _playerProgress.Stage+1; i <= Chapters[selectedChapter].StageQuantity-1 ; i++)
        {
            StageList.TryGetComponent(out StageUI stage);
            stage.stageBtnList[i].gameObject.SetActive(false);
            Debug.Log(stage.stageBtnList[i].gameObject.name);


        }
    }

    private void UpdateChapterTxt()
    {
        ChapterTxt.text = "Chapter. " + (CurrentSelectChap + 1).ToString();
    }
    private void ButtonUpdate()
    {
        if(CurrentSelectChap == _playerProgress.ChapterIndex)
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
    public string ChapterID;
    public int Stage;
    public Progress(int chapterIndex, string chapterID, int stage)
    {
        ChapterIndex = chapterIndex;
        ChapterID = chapterID;
        Stage = stage;
    }
    public Progress() { }
}
