using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private int ChapterIndex;
    [SerializeField] private List<MapSO> mapList;
    public List<StageSingleUI> stageBtnList;
    public RectTransform StageContainer;
    public ScrollRect StageContainerRect;
    public Image bg;
    [SerializeField] private List<Progress> playerProgress;


    public void Initialized()
    {
        playerProgress = GameManager.Instance._playerDataManager.PlayerDataSO.PlayerProgress;
        if (GameManager.Instance._playerDataManager.PlayerDataSO.IsHaveChapter(ChapterIndex))
        {
            //show rating
            for (int i = 0; i < stageBtnList.Count; i++)
            {
                if (i < playerProgress[ChapterIndex].StageList.Count)
                {
                    stageBtnList[i].Initialize(mapList[i], playerProgress[ChapterIndex].StageList[i].Rating);
                }
                else
                {
                    stageBtnList[i].Initialize(mapList[i], 0);

                }
            }
            

        }
        else
        {
            for (int i = 0; i < stageBtnList.Count; i++)
            {
               
                  stageBtnList[i].Initialize(mapList[i], 0);
               
                
            }
        }
                  DisableLockedStage(ChapterIndex);

       
        
    }

    public void DisableLockedStage(int currentChapter)
    {
        //unlock all
        if (currentChapter < playerProgress.Count - 1) return;
        //unlock first stage
        if(currentChapter > playerProgress.Count-1)
        {
            for(int i = 1; i < stageBtnList.Count; i++)
            {
                stageBtnList[i].gameObject.SetActive(false);
                Debug.Log(i);

            }
            return;
        }
        //unlock the rest
        for (int i = playerProgress[currentChapter].StageList.Count+1; i < stageBtnList.Count; i++)
        {
            Debug.Log(i);
            stageBtnList[i].gameObject.SetActive(false);
           


        }
    }

}
