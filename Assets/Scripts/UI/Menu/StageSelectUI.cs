using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
    [SerializeField] private Button ChapterExpandBtn;
    [SerializeField] private GameObject ChapterContainer;
    [SerializeField] private StageProgress stageProgress;
    [SerializeField] public GameObject StageList;
    [SerializeField] public List<ChapterElementUI> ChapterList;
   
    // Start is called before the first frame update
    void Start()
    {
        BackBtn.onClick.AddListener(() =>
        {
            Debug.Log("back");
        });
        HomeBtn.onClick.AddListener(() =>
        {
            Debug.Log("home");
            //do nothing
        });
        ChapterExpandBtn.onClick.AddListener(() =>
        {
            Debug.Log("close");

            ChapterContainer.SetActive(!ChapterContainer.activeSelf);
        });
        for(int i = 0; i < ChapterList.Count; i++)
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
