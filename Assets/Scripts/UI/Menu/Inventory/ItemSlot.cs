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
        QuantityTxt.text = item.Quantity.ToString();
    }
}
