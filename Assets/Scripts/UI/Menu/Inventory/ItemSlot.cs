using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image ItemSprite;
    [SerializeField] private TextMeshProUGUI QuantityTxt;
    public void Init(ItemsData item)
    {
        ItemSprite.sprite = item.Material.ItemSprite;
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