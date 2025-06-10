using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : UICanvas
{
    [SerializeField] private List<ItemSlot> itemSlotList = new List<ItemSlot>();
    [SerializeField] private List<ItemsData> Items;
    [SerializeField] private ItemSlot Prefab;
    [SerializeField] private Transform Container;
    //navigate btn
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
    //filter item btn
    [SerializeField] private Button ShowAll;//show all item
    [SerializeField] private Button ShowMaterial;//show item with material tag
    [SerializeField] private Button ShowConsume;//show item with consumabl tag

    private void Start()
    {
        BackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close<InventoryUI>(0);
        });
        HomeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ToHomeMenu();
        });
        ShowAll.onClick.AddListener(() => { });
        ShowMaterial.onClick.AddListener(() => { });
        ShowConsume.onClick.AddListener(() => { });
        GameManager.Instance._playerDataManager.OnDataChange += _playerDataManager_OnDataChange;
    }
    private void OnDisable()
    {
        GameManager.Instance._playerDataManager.OnDataChange -= _playerDataManager_OnDataChange;

    }
    private void _playerDataManager_OnDataChange(object sender, System.EventArgs e)
    {
        UpdateItem();
    }

    public override void SetUp()
    {
        Initialized();
    }
    private void Initialized()
    {
        Items = GameManager.Instance._playerDataManager.PlayerDataSO.Items;
        for(int i = 0; i < Items.Count; i++)
        {
            ItemSlot item = Instantiate(Prefab, Container);
            item.Init(Items[i]);    
            itemSlotList.Add(item);
        }
    }
    private void ClearItems()
    {
        for(int i = 0;i < Container.childCount; i++)
        {
            Destroy(Container.GetChild(i).gameObject);
        }
        
    }
    private void UpdateItem()
    {
        ClearItems();
        Items = GameManager.Instance._playerDataManager.PlayerDataSO.Items;
        for (int i = 0; i < Items.Count; i++)
        {
            ItemSlot item = Instantiate(Prefab, Container);
            item.Init(Items[i]);
            itemSlotList.Add(item);
        }
    }
}
