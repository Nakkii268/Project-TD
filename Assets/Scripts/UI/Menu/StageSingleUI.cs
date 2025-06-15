using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSingleUI : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private TextMeshProUGUI stageName;
    [SerializeField] private Image stageRating;
    
    public void Initialize(MapSO map,int rating)
    {
        stageName.text = map.MapName;
        btn.onClick.AddListener(() =>
        {
            GameManager.Instance._sceneLoader.LoadStage(map.StagePath);
            UIManager.Instance.OpenUI<StageLoadingUI>(map.MapName);
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
