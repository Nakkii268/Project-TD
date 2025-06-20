using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSingleUI : MonoBehaviour
{
    
    [SerializeField] public Button btn;
    [SerializeField] private TextMeshProUGUI stageName;
    [SerializeField] private Image stageRating;
    public event EventHandler<MapSO> OnStageSelect;

    public void Initialize(MapSO map,int rating)
    {
        stageName.text = map.MapID;
        btn.onClick.AddListener(() =>
        {
            OnStageSelect?.Invoke(this, map);
        });
        switch (rating)
        {
            case 0: stageRating.fillAmount= 0; break;
            case 1: stageRating.fillAmount= 0.674f; break;
            case 2: stageRating.fillAmount = 1; break;
             default: break;
        }
    }
}
