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
    [SerializeField] private TextMeshProUGUI PlayerStamina;
    [SerializeField] private TextMeshProUGUI StaminaCost;
    public void Init(MapSO map)
    {
        StageID.text = map.MapID;
        StageName.text = map.MapName;
        PrepareBtn.onClick.AddListener(() =>
        {
            if (GameManager.Instance._staminaManager.StaminaConsumed(map.StaminaCost))
            {
                UIManager.Instance.OpenUI<PreBattleLineUpUI>(map);

            }
        });
        ClearChild();
        for(int i=0; i < map.DropItem.Count; i++)
        {
            ItemDrop drop = Instantiate(DropPrefab, DropPreviewContainer);
            drop.Init(map.DropItem[i]);
            
        }
        StaminaCost.text = map.StaminaCost.ToString();
        UpdateStaminaTxt();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance._playerDataManager.OnDataChange += _playerDataManager_OnDataChange;
    }

  

    protected override void OnDisable()
    {
        base.OnDisable();
        GameManager.Instance._playerDataManager.OnDataChange -= _playerDataManager_OnDataChange;

    }
    private void _playerDataManager_OnDataChange(object sender, EventArgs e)
    {
        UpdateStaminaTxt();

    }
    protected override void PointerClickHandler_OnPointerClick(object sender, EventArgs e)
    {
        if (!isPointerIn)
        {
           
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
    private void UpdateStaminaTxt()
    {
        PlayerStamina.text = GameManager.Instance._staminaManager.GetStaminaTxt();
    }
}
