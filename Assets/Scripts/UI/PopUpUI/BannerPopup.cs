using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BannerPopup : UICanvas
{
    [SerializeField] private TextMeshProUGUI content;
    [SerializeField] private float closeTime;
    public override void SetUp(object t)
    {
        string ct = t as string;
        Init(ct);
    }
    private void Init(string text)
    {
        content.text = text;
        Invoke("CloseUI",closeTime);
    }
    private void  CloseUI()
    {
     
        UIManager.Instance.Close<BannerPopup>(0);
    }
}
