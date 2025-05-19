using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSingleUI : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private TextMeshProUGUI stageName;
    
    public void Initialize(MapSO map)
    {
        stageName.text = map.MapName;
        btn.onClick.AddListener(() =>
        {
            GameManager.Instance._sceneLoader.LoadStage(map.StagePath);
            UIManager.Instance.OpenUI<StageLoadingUI>(map.MapName);
        });
    }
}
