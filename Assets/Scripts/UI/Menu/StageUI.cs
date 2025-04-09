using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private List<MapSO> mapList;
    public List<StageSingleUI> stageBtnList;
    public RectTransform StageContainer;


    private void Start()
    {
        Initialized();
    }
    private void Initialized()
    {
        for (int i = 0; i < stageBtnList.Count; i++) {
            stageBtnList[i].Initialize(mapList[i]);
        }
    }
}
