using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    [SerializeField] private List<MapSO> mapList;
    public List<StageSingleUI> stageBtnList;
    public RectTransform StageContainer;
    public ScrollRect StageContainerRect;
    public Image bg;


    private void Start()
    {
        Initialized();
        StageContainerRect.onValueChanged.AddListener(OnScrollChange);
    }
    private void Initialized()
    {
        for (int i = 0; i < stageBtnList.Count; i++) {
            stageBtnList[i].Initialize(mapList[i]);
        }
    }
    private void OnScrollChange(Vector2 value)
    {
        if(value.x <= .5f)
        {
            bg.color = Color.white;
        }
        else
        {
            bg.color = Color.red;
        }
    }
    
  
}
