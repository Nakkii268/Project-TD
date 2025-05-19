using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageLoadingUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI stageName;


    public override void SetUp(object para)
    {
        Initialized(para);
    }

    private void Initialized(object s)
    {
        if(s is string txt)
        {
            stageName.text = txt;
        }
        
    }
}
