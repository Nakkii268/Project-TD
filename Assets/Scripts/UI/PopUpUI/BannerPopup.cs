using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BannerPopup : UICanvas
{
    [SerializeField] private Transform _TextPopUp;
    [SerializeField] private Transform _DropPopUp;
    [SerializeField] private TextMeshProUGUI TextContent;
    [SerializeField] private ItemDrop DropPrefab;
    [SerializeField] private float closeTime;
    public override void SetUp(object t)
    {
        if (t is string)
        {
            string ct = t as string;
            TextPopup(ct);
        }else if(t is List<ItemsData>)
        {
            List<ItemsData> data = t as List<ItemsData>;
            ItemPopUp(data);
        }
    }
    
   
    private void  CloseUI()
    {
     
        UIManager.Instance.Close<BannerPopup>(0);
    }

    private void TextPopup(string content)
    {
        TextContent.text = content;
        _TextPopUp.gameObject.SetActive(true);
        Invoke("CloseUI", closeTime);

    }
    private void ItemPopUp(List<ItemsData> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            ItemDrop drop = Instantiate(DropPrefab,_DropPopUp);
            drop.Init(data[i]);
        }
        _DropPopUp.gameObject.SetActive(true);
        Invoke("CloseUI", closeTime);

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CloseUI();
        }
    }
}
