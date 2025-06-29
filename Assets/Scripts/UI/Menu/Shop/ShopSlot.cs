using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] private ShopSlotData slotData;
    [SerializeField] private Button _btn;
    [SerializeField] private Button viewBtn;
    [SerializeField] private Image ItemSprite;
    [SerializeField] private Image CurrencySprite;
    [SerializeField] private TextMeshProUGUI ItemPrice;
    [SerializeField] private TextMeshProUGUI ItemName;
    [SerializeField] private TextMeshProUGUI AvailableQtt;
    [SerializeField] private TextMeshProUGUI QuantityTxt;
    [SerializeField] private Transform OutofStockUI;
    private string BuySuccessTxt = "Successfully buy";
    private string BuyFailedTxt = "Not enought currrency";


   

    public void Init(ShopSlotData data)
    {
        slotData = data;
        ItemSprite.sprite = data.Item.ItemSprite;
        ItemPrice.text = data.Price.ToString();
        AvailableQtt.text = data.AvailableQtt.ToString()+"/"+data.MaxQtt.ToString();
        QuantityTxt.text = data.Quantity.ToString();
        ItemName.text = data.Item.ItemName;
        CurrencySprite.sprite = data.Currency.ItemSprite;
        BuyBtnHandle();
    }
    private void Start()
    {
        _btn.onClick.AddListener(() =>
        {
            if (GameManager.Instance._playerDataManager.PlayerDataSO.IsHaveItem(slotData.Currency.ItemID) >= slotData.Price)
            {
                slotData.AvailableQtt--;
                GameManager.Instance._playerDataManager.PlayerDataSO.AddItem(slotData.Item, slotData.Quantity);
                GameManager.Instance._playerDataManager.PlayerDataSO.RemoveItem(slotData.Currency.ItemID, slotData.Price);
                GameManager.Instance._playerDataManager.PlayerDataSO.UpdateShopItem(slotData.Item.ItemID, slotData.AvailableQtt);
                UpdateAvailabelTxt();
                UIManager.Instance.OpenUI<BannerPopup>(BuySuccessTxt);
                BuyBtnHandle();
                
            }
            else
            {
                UIManager.Instance.OpenUI<BannerPopup>(BuyFailedTxt);

            }
        });
        viewBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<ItemPopupUI>(new ItemPopupData(slotData.Item, false));
        });
    }
    private void BuyBtnHandle()
    {
        if (slotData.AvailableQtt <= 0) { 
            _btn.interactable = false;
            OutofStockUI.gameObject.SetActive(true);
        }
    }
    private void UpdateAvailabelTxt()
    {
        AvailableQtt.text = slotData.AvailableQtt.ToString() + "/" + slotData.MaxQtt.ToString();

    }
}
