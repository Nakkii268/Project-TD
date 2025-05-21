using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialRequiredSingle : MonoBehaviour
{
    [SerializeField] private Image ItemIcon;
    [SerializeField] private TextMeshProUGUI ItemQttTxt;
    [SerializeField] private TextMeshProUGUI ItemRqTxt;

    public void Init(ItemsData require, int qtt)
    {
        ItemIcon.sprite = require.Material.ItemSprite;
        ItemQttTxt.text = qtt.ToString();
        ItemRqTxt.text = "/" + require.Quantity.ToString();
        if (require.Quantity > qtt) {
            ItemQttTxt.color = Color.red;
        }
        else
        {
            ItemQttTxt.color = Color.white;
        }
    }

}
