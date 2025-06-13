using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Item SlotItem;
    [SerializeField] private Image ItemSprite;
    [SerializeField] private TextMeshProUGUI QuantityTxt;
    private void Start()
    {
        _btn.onClick.AddListener(() =>
        {
            ConsumableItem cItem = (ConsumableItem)SlotItem;
            cItem.OnUse();
        });
    }
    public void Init(ItemsData item)
    {
        SlotItem = item.Item;
        ItemSprite.sprite = item.Item.ItemSprite;
        QuantityTxt.text = ShortenQuantity(item.Quantity);
    }
    private string ShortenQuantity(int qtt)
    {
        if (qtt >= 10000 && qtt < 1000000)
        {
            return (qtt / 10000).ToString() + "K";
        }
        else if (qtt >= 1000000)
        {
            return (qtt / 1000000).ToString() + "M";

        }
        return qtt.ToString();
    }
}