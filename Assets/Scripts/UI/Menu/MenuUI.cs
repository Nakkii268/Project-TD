using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : UICanvas
{
    

    [Header("Button")]
    [SerializeField] private Button Battle;
    [SerializeField] private Button LineUp;
    [SerializeField] private Button Character;
    [SerializeField] private Button Wish;
    [SerializeField] private Button Shop;
    [SerializeField] private Button Quest;
    [SerializeField] private Button Bag;
    [SerializeField] private Button Mail;
    [SerializeField] private Button Option;
    [SerializeField] private Button BuyGold;
    [SerializeField] private Button BuyDiamond;
    [Header("Currency")]
    [SerializeField] private TextMeshProUGUI Gold;
    [SerializeField] private TextMeshProUGUI Diamond;
    [Header("Infomation")]
    [SerializeField] private TextMeshProUGUI PlayerName;
    [SerializeField] private TextMeshProUGUI PlayerLevel;
    [SerializeField] private Image PlayerLevelProgress;



    private void Start()
    {


        GameManager.Instance._playerDataManager.OnDataChange += _playerDataManager_OnDataChange;
        Battle.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<StageSelectUI>();
            
            
        });
        Character.onClick.AddListener(() =>
        {
           UIManager.Instance.OpenUI<UnitListUI>();
            

           

        });
        LineUp.onClick.AddListener(() => { 
            UIManager.Instance.OpenUI<LineUpUI>();
        });
        Bag.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<InventoryUI>();
        });
        Shop.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<ShopUI>();
        });
        Quest.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<QuestUI>();
        });
    }
    private void OnDisable()
    {
        GameManager.Instance._playerDataManager.OnDataChange -= _playerDataManager_OnDataChange;

    }

    private void _playerDataManager_OnDataChange(object sender, System.EventArgs e)
    {
        Initialized();
    }

    private void Initialized() {
       //player name, level, progress
       Gold.text =GameManager.Instance._playerDataManager.PlayerDataSO.IsHaveItem("G01").ToString();
       Diamond.text =GameManager.Instance._playerDataManager.PlayerDataSO.IsHaveItem("D01").ToString();
        PlayerName.text = GameManager.Instance._playerDataManager.PlayerDataSO.PlayerName;
    }
    public override void SetUp()
    {
        Initialized();
    }


}
