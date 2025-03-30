using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterElementUI : MonoBehaviour
{
  
    public ChapterSO ChapterSO;
    public Button btn;
    public event EventHandler<string> OnChapterSelect;
    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            OnChapterSelect?.Invoke(this, ChapterSO.StageUIPath);
        });
    }

    //somthing like render chapter img, name
}
