using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitListUI : MonoBehaviour
{
    [SerializeField] public CharacterInfoUI characterInfoUI;
    [SerializeField] private Transform container;
    [SerializeField] private List<AllianceUnit> unitList;//test
    [SerializeField] private UnitListSingleUI singlePrefab;//test
    [SerializeField] private List<UnitListSingleUI> SingleList;//test
    [SerializeField] private ScrollRect scrollRect;
    private void Initialized()
    {
        //get unitlist from player data ** temporarily using preset data
        for(int i = 0;i < unitList.Count; i++)
        {
            UnitListSingleUI single = Instantiate(singlePrefab, container);
            SingleList.Add(single);
            single.Initialized(unitList[i]);
        }
        scrollRect.horizontalNormalizedPosition = 0;
    }
    private void Start()
    {
        Initialized();
    }

    //filter by tag ** maybe just skip** 
    //sort by level
}
