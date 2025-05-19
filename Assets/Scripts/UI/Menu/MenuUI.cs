using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : UICanvas
{
    

    [Header("Button")]
    [SerializeField] private Button Battle;
    [SerializeField] private Button LineUp;
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
            
            
        });
        Character.onClick.AddListener(() =>
        {
           UIManager.Instance.OpenUI<UnitListUI>();
            

           

        });
        LineUp.onClick.AddListener(() => { 
            UIManager.Instance.OpenUI<LineUpUI>();
        });
        Bag.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<LoseUI>();
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
