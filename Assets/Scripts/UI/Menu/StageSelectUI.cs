using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class StageSelectUI : UICanvas
{
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;

    [SerializeField] private Button ChapterExpandBtn;
    [SerializeField] private GameObject ChapterContainer;
    [SerializeField] private StageProgress stageProgress;
    [SerializeField] public GameObject StageList;
    [SerializeField] public List<ChapterElementUI> ChapterList;
    [SerializeField] private Image chapterExpandIcon;
   
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
        ChapterExpandBtn.onClick.AddListener(() =>
        {
            if (!ChapterContainer.gameObject.activeInHierarchy)
            {
                chapterExpandIcon.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
            }
            else
            {
                chapterExpandIcon.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

            }

            ChapterContainer.SetActive(!ChapterContainer.activeSelf);
        });
       

    }
    public override void SetUp()
    {
        Initialized();
    }
    private void Initialized()
    {
        for (int i = 0; i < ChapterList.Count; i++)
        {
            ChapterList[i].OnChapterSelect += StageSelectUI_OnChapterSelect;

        }
        LoadStageList(stageProgress.LastStage.Chapter);

        stageProgress.DisableLockedChapter();
    }

    private void StageSelectUI_OnChapterSelect(object sender, ChapterSO e)
    {
       LoadStageList(e);
      


    }

    public void LoadStageList(ChapterSO chapter)
    {
        Addressables.LoadAssetAsync<GameObject>(chapter.StageUIPath).Completed += ((handler) =>
        {
            if (handler.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                GameObject newStageList = Instantiate(handler.Result, transform);
                newStageList.transform.SetAsFirstSibling();
                Destroy(StageList);
                StageList = newStageList;
                Debug.Log(StageList.name);
                stageProgress.DisableLockedStage(chapter.ChapterIndex);


            }
        });
        
    }
   

}
