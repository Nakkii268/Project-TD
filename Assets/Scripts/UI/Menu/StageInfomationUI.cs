using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageInfomationUI : MonoBehaviour
{
    
    [SerializeField] private Button PrepareBtn;

    public void Init(MapSO map)
    {
        PrepareBtn.onClick.AddListener(() =>
        {
            
            UIManager.Instance.OpenUI<PreBattleLineUpUI>(map);
        });
    }
}
