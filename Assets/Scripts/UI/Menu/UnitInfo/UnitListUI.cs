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
    [SerializeField] public Button BackBtn;
    [SerializeField] public Button HomeBtn;
    private void Initialized()
    {
        //get unitlist from player data ** temporarily using preset data
        for(int i = 0;i < unitList.Count; i++)
        {
            UnitListSingleUI single = Instantiate(singlePrefab, container);
            SingleList.Add(single);
            single.Initialized(unitList[i]);
            single.gameObject.SetActive(true);
        }
        scrollRect.horizontalNormalizedPosition = 0;
        BackBtn.onClick.AddListener(CloseUI);
        HomeBtn.onClick.AddListener(GoToHome);
        characterInfoUI = MenuUIManager.Instance.CharacterInfoUI;
    }
    private void Start()
    {
        Initialized();
        
    }
    private void OnDisable()
    {
        BackBtn.onClick.RemoveListener(CloseUI);
        HomeBtn.onClick.RemoveListener(GoToHome);
    }
    public void CloseUI()
    {
        this.gameObject.SetActive(false);
    }
    public void GoToHome()
    {
        this.gameObject.SetActive(false);

        MenuUIManager.Instance.gameObject.SetActive(true);
    }
    //filter by tag ** maybe just skip** 
    //sort by level
}
