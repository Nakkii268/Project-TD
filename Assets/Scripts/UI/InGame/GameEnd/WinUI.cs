using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : UICanvas
{
    [SerializeField] private Button exitBtn;
    [SerializeField] private TextMeshProUGUI stageName;
    [SerializeField] private ItemDrop prefab;
    [SerializeField] private Transform container;

    public override void SetUp(object t)
    {
        if ( t is MapData map) {
            Initialized(map);
        }
    }
    private void Start()
    {
        exitBtn.onClick.AddListener(() =>
        {
            GameManager.Instance._sceneLoader.LoadMenu();

            UIManager.Instance.OpenUI<StageSelectUI>();
        });
      
        exitBtn.interactable = false;
        StartCoroutine(EnableExitBtn());
    }

    private IEnumerator EnableExitBtn()
    {
        yield return new WaitForSeconds(1);
        exitBtn.interactable = true;
    }

    private void Initialized(MapData mapData)
    {
        stageName.text  = mapData.Map.MapName;
        for (int i = 0; i < mapData.Map.DropItem.Count; i++)
        {
            ItemDrop drop = Instantiate(prefab, container);
            drop.Init(mapData.Map.DropItem[i]);
        }
    }
}
[Serializable]
public class MapData
{
    public MapSO Map;
    public int Rating;
    public MapData(MapSO map, int rating)
    {
        Map = map;
        Rating = rating;
    }
}