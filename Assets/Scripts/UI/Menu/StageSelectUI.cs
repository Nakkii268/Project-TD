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
    [SerializeField] private GameObject StageList;
    [SerializeField] private List<ChapterElementUI> ChapterList;
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

    }

 

    private void StageSelectUI_OnChapterSelect(object sender, string e)
    {
       LoadStageList(e);
    }

    public void LoadStageList(string path)
    {
        Addressables.LoadAssetAsync<GameObject>(path).Completed += StageSelectUI_Completed;
    }

    private void StageSelectUI_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            GameObject newStageList = Instantiate(obj.Result, transform);
            newStageList.transform.SetAsFirstSibling();
            Destroy(StageList);
            StageList = newStageList;
        }
    }
}
