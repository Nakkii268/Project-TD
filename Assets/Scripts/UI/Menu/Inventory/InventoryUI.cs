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
}
