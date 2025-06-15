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
    [SerializeField] private Button ShowCurrency;//show item with consumabl tag
    [SerializeField] private List<GameObject> CategoryBtnActive; //index of category +1

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
        ShowAll.onClick.AddListener(() => {
            UpdateItem(-1);

        });
        ShowMaterial.onClick.AddListener(() => {
            UpdateItem(2);

        });
        ShowConsume.onClick.AddListener(() => {
            UpdateItem(1);

        });
        ShowCurrency.onClick.AddListener(() => {
            UpdateItem(0);

        });
        GameManager.Instance._playerDataManager.OnDataChange += _playerDataManager_OnDataChange;
    }
    private void OnDisable()
    {
        GameManager.Instance._playerDataManager.OnDataChange -= _playerDataManager_OnDataChange;

    }
    private void _playerDataManager_OnDataChange(object sender, System.EventArgs e)
    {
        UpdateItem(-1);
    }

    public override void SetUp()
    {
        Initialized();
    }
    private void Initialized()
    {
        Items = GameManager.Instance._playerDataManager.PlayerDataSO.Items;
        UpdateItem(-1);
    }
    private void ClearItems()
    {
        for(int i = 0;i < Container.childCount; i++)
        {
            Destroy(Container.GetChild(i).gameObject);
        }
        
    }
    private void UpdateItem(int category) //-1 =all, 0=cur, 1=con,2 =mat
    {
        ClearItems();
        Items = GameManager.Instance._playerDataManager.PlayerDataSO.Items;
        for (int i = 0; i < Items.Count; i++)
        {
            
            if ((int)Items[i].Item.Category == category|| category==-1)
            {
                ItemSlot item = Instantiate(Prefab, Container);
                item.Init(Items[i]);
                itemSlotList.Add(item);
            }
        }
        HandleCategoryBtn(category);
    }
    private void HandleCategoryBtn(int category)
    {
        for(int i = 0;i< CategoryBtnActive.Count; i++)
        {
            if (i == category + 1)
            {
                CategoryBtnActive[i].SetActive(true);
            }
            else
            {
                CategoryBtnActive[i].SetActive(false);
            }
        }
    }
}
