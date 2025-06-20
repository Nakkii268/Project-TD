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
    [SerializeField] private TextMeshProUGUI StageID;
    [SerializeField] private Transform DropPreviewContainer;
    [SerializeField] private ItemDrop DropPrefab;

    public void Init(MapSO map)
    {
        StageID.text = map.MapID;
        StageName.text = map.MapName;
        PrepareBtn.onClick.AddListener(() =>
        {
            
            UIManager.Instance.OpenUI<PreBattleLineUpUI>(map);
        });
        ClearChild();
        for(int i=0; i < map.DropItem.Count; i++)
        {
            ItemDrop drop = Instantiate(DropPrefab, DropPreviewContainer);
            drop.Init(map.DropItem[i]);
            
        }
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
    private void ClearChild()
    {
        for(int i=0; i< DropPreviewContainer.childCount;i++)
        {
            Destroy(DropPreviewContainer.GetChild(i).gameObject);
        }
    }
}
