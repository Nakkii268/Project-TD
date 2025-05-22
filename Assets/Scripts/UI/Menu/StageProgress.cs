using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProgress : MonoBehaviour
{

    [SerializeField] private Progress CurrentProgress;
    [SerializeField] public Progress LastStage; //last stage player complete;
    [SerializeField] private StageSelectUI _stageSelectUI;
    [SerializeField]private int MaxChapter;
    public void DisableLockedChapter()
    {
        for (int i = GameManager.Instance._resourceManager.GetChapterById<ChapterSO>(CurrentProgress.ChapterID).ChapterIndex+1; i <= MaxChapter-1; i++)
        {
            _stageSelectUI.ChapterList[i].btn.interactable = false;
        }
        
    }

    public void DisableLockedStage(int selectedChapter)
    {
        Debug.Log("call");
        if (selectedChapter < GameManager.Instance._resourceManager.GetChapterById<ChapterSO>(CurrentProgress.ChapterID).ChapterIndex) return;
        Debug.Log("run");
        
        for (int i = CurrentProgress.Stage ; i <= _stageSelectUI.ChapterList[selectedChapter].ChapterSO.StageQuantity-1; i++)
        {
            _stageSelectUI.StageList.TryGetComponent(out StageUI stage);
            stage.stageBtnList[i].gameObject.SetActive(false);
            Debug.Log(stage.stageBtnList[i].gameObject.name);


        }//9  1 2 3 4 5 6 7 8 9 
        //    0 1 2 3 4 5 6 7 8
    }
}
[Serializable]
public class Progress
{
    public string ChapterID;
    public int Stage;
}
