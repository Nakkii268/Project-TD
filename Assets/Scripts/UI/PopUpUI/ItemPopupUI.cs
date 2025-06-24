using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopupUI : UICanvas
{
    [SerializeField] private Image ItemSprite;
    [SerializeField] private TextMeshProUGUI ItemQtt;
    [SerializeField] private TextMeshProUGUI ItemName;
    [SerializeField] private TextMeshProUGUI ItemDesc;
    [SerializeField] private Button ConfirmBtn;
    //Item patch use, item from inventory only
    [SerializeField] private TextMeshProUGUI ConfirmBtntxt;
    [SerializeField] private string USE_TXT = "USE";
    [SerializeField] private string CONFIRM_TXT = "CONFRIM";
    
    public override void SetUp(object t)
    {
        ItemPopupData data = t as ItemPopupData;
        Initialized(data );
    }


    private void Initialized(ItemPopupData data)
    {
        ItemSprite.sprite = data.Item.ItemSprite;
        ItemName.text = data.Item.ItemName;
        ItemDesc.text = data.Item.Description;
        ItemQtt.text = "";

        if (data.Source)
        {
            ItemQtt.text = "Quantity: " + data.Qtt.ToString();

        }
        ConfirmBtn.onClick.AddListener(() =>
        {
            if (data.Source)
            {
                if (data.Item.Category == ItemCategory.Consumable)
                {
                    
                    ConsumableItem cItem = (ConsumableItem)data.Item;
                    cItem.OnUse();
                }
            }
            UIManager.Instance.Close<ItemPopupUI>(0);
        });
    }
}
[Serializable]
public class ItemPopupData
{
    public Item Item;
    public int Qtt;
    public bool Source; //true: open call from inventory || false: shop/somewhere else that just allown to see detail
    public ItemPopupData(Item item, bool source, int qtt=0)
    {
        this.Item = item;
        Qtt = qtt;
        Source = source;
    }
}