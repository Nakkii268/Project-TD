using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSingleUI : MonoBehaviour
{
    [SerializeField] private Button btn;
    [SerializeField] private Image stageImg;
    
    public void Initialize(MapSO map)
    {
        stageImg.sprite = map.MapIng;
        btn.onClick.AddListener(() =>
        {
            GameManager.Instance._sceneLoader.LoadStage(map.StagePath);
            UIManager.Instance.OpenUI<StageLoadingUI>(map.MapName);
        });
    }
}
