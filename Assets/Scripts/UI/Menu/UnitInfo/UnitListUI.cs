using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitListUI : UICanvas
{
    
    [SerializeField] private Transform container;
    [SerializeField] private List<AllianceUnit> unitList;//test
    [SerializeField] private UnitListSingleUI singlePrefab;//test
    [SerializeField] private List<UnitListSingleUI> SingleList;//test
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] public Button BackBtn;
    [SerializeField] public Button HomeBtn;
    private void Initialized()
    {
       UpdateUI();
      
    }
    private void Start()
    {
        GameManager.Instance._playerDataManager.OnDataChange += _playerDataManager_OnDataChange;
        BackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close<UnitListUI>(0);
        });
        HomeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ToHomeMenu();
        });

    }
    private void UpdateUI()
    {
        ClearChild();
        unitList = GameManager.Instance._playerDataManager.PlayerDataSO.OwnedCharacter;
        for (int i = 0; i < unitList.Count; i++)
        {
            UnitListSingleUI single = Instantiate(singlePrefab, container);
            SingleList.Add(single);
            single.Initialized(unitList[i]);
            single.gameObject.SetActive(true);
        }
        scrollRect.horizontalNormalizedPosition = 0;
    } 

    private void _playerDataManager_OnDataChange(object sender, System.EventArgs e)
    {
        UpdateUI();
    }

    
    private void OnDisable()
    {
        for(int i = 0; i < container.childCount; i++)
        {
            Destroy(container.GetChild(i).gameObject);
        }
      
        SingleList.Clear();
        GameManager.Instance._playerDataManager.OnDataChange -= _playerDataManager_OnDataChange;

    }
    public override void SetUp()
    {
        Initialized();

    }
    private void ClearChild()
    {
        for(int i = 0;i< container.childCount; i++)
        {
            Destroy(container.GetChild(i).gameObject);
        }
    }
    //filter by tag ** maybe just skip** 
    //sort by level
}
