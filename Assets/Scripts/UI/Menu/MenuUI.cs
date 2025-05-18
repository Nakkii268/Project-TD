using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : UICanvas
{
    

    [Header("Button")]
    [SerializeField] private Button Battle;
    [SerializeField] private Button Squad;
    [SerializeField] private Button Character;
    [SerializeField] private Button Wish;
    [SerializeField] private Button Shop;
    [SerializeField] private Button Quest;
    [SerializeField] private Button Bag;
    [SerializeField] private Button Mail;
    [SerializeField] private Button Option;
    [SerializeField] private Button BuyGold;
    [SerializeField] private Button BuyDiamond;
    [Header("Currency")]
    [SerializeField] private TextMeshProUGUI Gold;
    [SerializeField] private TextMeshProUGUI Diamond;
    [Header("Infomation")]
    [SerializeField] private TextMeshProUGUI PlayerName;
    [SerializeField] private TextMeshProUGUI PlayerLevel;
    [SerializeField] private Image PlayerLevelProgress;



    private void Start()
    {
       
        
      
        Battle.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<StageSelectUI>();
            
            Debug.Log("clicked");
        });
        Character.onClick.AddListener(() =>
        {
           UIManager.Instance.OpenUI<UnitListUI>();
            

            Debug.Log("clicked1");

        });
    }
    private void Initialized() {
       //player name, level, progress
    }
    public override void SetUp()
    {
        Initialized();
    }


}
