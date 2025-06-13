using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UICanvas
{
  //  [SerializeField] private ShopSO CharacterShop;
    [SerializeField] private ShopSO _shopSO;
    [SerializeField] private ShopSlot Prefab;
    [SerializeField] private List<ShopSlot> Slots;
    [SerializeField] private Transform Container;
    //navigate btn
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;

    //switch tab btn
    [SerializeField] private Button CharBtn;
    [SerializeField] private Button MaterialBtn;
    [SerializeField] private int CurrentTabIndex=0;
  

    //player currency
    [SerializeField] private TextMeshProUGUI PlayerGold;
    [SerializeField] private TextMeshProUGUI PlayerDianmond;
    
    private void Start()
    {

        BackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close<ShopUI>(0);
           

        });
        HomeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ToHomeMenu();
        });
        GameManager.Instance._playerDataManager.OnDataChange += _playerDataManager_OnDataChange;
        MaterialBtn.onClick.AddListener(() => {
            if (CurrentTabIndex == 0) return;
            CategoryBtnHandle(0);
            CurrentTabIndex = 0;
            Initialized(0);
        });
        CharBtn.onClick.AddListener(() => {
            if (CurrentTabIndex == 1) return;
            CategoryBtnHandle(1);

            CurrentTabIndex = 1;
            Initialized(1);
        });
    }

    private void _playerDataManager_OnDataChange(object sender, System.EventArgs e)
    {
        UpdateCurrency();

    }

    public override void SetUp()
    {
        Initialized(0);
    }
    private void Initialized(int shopCate)
    {
        UpdateCurrency();
        ClearItem();
        if (shopCate == 0)
        {

            for (int i = 0; i < _shopSO.Slots.Count; i++)
            {
                if (_shopSO.Slots[i].Category == ShopCategory.MatetialShop)
                {
                    ShopSlot slot = Instantiate(Prefab, Container);
                    slot.Init(_shopSO.Slots[i]);
                    Slots.Add(slot);
                }
            }
        }
        else if (shopCate == 1) { 

            for (int i = 0; i < _shopSO.Slots.Count; i++)
            {
                if (_shopSO.Slots[i].Category == ShopCategory.CharacterShop)
                {
                    ShopSlot slot = Instantiate(Prefab, Container);
                    slot.Init(_shopSO.Slots[i]);
                    Slots.Add(slot);
                }
                
            }
     }

    }

    private void ClearItem()
    {
        for(int i = 0;i < Container.childCount; i++)
        {
            Destroy(Container.GetChild(i).gameObject);   
        }
        Slots.Clear();
    }
    private void UpdateCurrency()
    {
        PlayerGold.text = GameManager.Instance._playerDataManager.PlayerDataSO.IsHaveItem("G01").ToString();
        PlayerDianmond.text = GameManager.Instance._playerDataManager.PlayerDataSO.IsHaveItem("D01").ToString();
    }
    private void CategoryBtnHandle(int index)
    {
        
    }
}
[Serializable]
public class ButtonData
{
    
    public TextMeshProUGUI _txt;
    public Button _btn;
    public Image _icon;
}