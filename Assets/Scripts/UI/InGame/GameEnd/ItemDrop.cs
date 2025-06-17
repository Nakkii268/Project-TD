using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private Image ItemSprite;
    [SerializeField] private TextMeshProUGUI ItemQuantity;

    public void Init(ItemsData item)
    {
        ItemSprite.sprite = item.Item.ItemSprite;
        ItemQuantity.text = item.Quantity.ToString();
    }
}
