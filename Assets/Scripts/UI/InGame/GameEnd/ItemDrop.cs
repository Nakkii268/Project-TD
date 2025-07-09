using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private Image ItemSprite;
    [SerializeField] private TextMeshProUGUI ItemQuantity;
    [SerializeField] private TextMeshProUGUI ExtraText;

    public void Init(ItemsData item)
    {
        ItemSprite.sprite = item.Item.ItemSprite;
        ItemQuantity.text = item.Quantity.ToString();
        ExtraText.text = "";

    }
    public void Init(ItemsData item,string extra)
    {
        ItemSprite.sprite = item.Item.ItemSprite;
        ItemQuantity.text = item.Quantity.ToString();
        ExtraText.text = extra.ToString();
    }
}
