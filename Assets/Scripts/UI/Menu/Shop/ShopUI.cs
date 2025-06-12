using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UICanvas
{
  //  [SerializeField] private ShopSO CharacterShop;
    [SerializeField] private ShopSO MaterialShop;
    [SerializeField] private ShopSlot Prefab;
    [SerializeField] private List<ShopSlot> Slots;
    [SerializeField] private Transform Container;
    //navigate btn
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;

    //switch tab btn
 //   [SerializeField] private Button CharBtn;
  //  [SerializeField] private Button MaterialBtn;
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
    }

    private void _playerDataManager_OnDataChange(object sender, System.EventArgs e)
    {
        UpdateCurrency();

    }

    public override void SetUp()
    {
        Initialized();
    }
    private void Initialized()
    {
        UpdateCurrency();
        ClearItem();
      /*  if (CurrentTabIndex == 0)
        {
           
            for (int i = 0; i < CharacterShop.Slots.Count; i++)
            {
                ShopSlot slot = Instantiate(Prefab, Container);
                slot.Init(CharacterShop.Slots[i]);
                Slots.Add(slot);
            }
        }
        else*/// if(CurrentTabIndex == 1)
        //{
            for (int i = 0; i < MaterialShop.Slots.Count; i++)
            {
                ShopSlot slot = Instantiate(Prefab, Container);
                slot.Init(MaterialShop.Slots[i]);
                Slots.Add(slot);
            }
      //  }

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
}
