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
    [SerializeField] private int qtt;
    private void Start()
    {
        _btn.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI < ItemPopupUI>(new ItemPopupData(SlotItem,true,qtt));
        });
    }
    public void Init(ItemsData item)
    {
        SlotItem = item.Item;
        ItemSprite.sprite = item.Item.ItemSprite;
        QuantityTxt.text = ShortenQuantity(item.Quantity);
        qtt = item.Quantity;
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