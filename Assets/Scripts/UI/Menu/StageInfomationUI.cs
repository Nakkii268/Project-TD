using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageInfomationUI : PointerDetect
{
    
    [SerializeField] private Button PrepareBtn;
    [SerializeField] private TextMeshProUGUI StageName;

    public void Init(MapSO map)
    {
        StageName.text = map.MapName;
        PrepareBtn.onClick.AddListener(() =>
        {
            
            UIManager.Instance.OpenUI<PreBattleLineUpUI>(map);
        });
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
    }
    protected override void PointerClickHandler_OnPointerClick(object sender, EventArgs e)
    {
        if (!isPointerIn)
        {
            Debug.Log("close Info Ui");
            gameObject.SetActive(false);
        }
    }
}
