using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public static MenuUIManager Instance;
    [SerializeField] private StageSelectUI _stageUI;
    [SerializeField] private UnitListUI _unitListUI;
    [SerializeField] private CharacterInfoUI _characterInfoUI;
    [SerializeField] private UnitLevelUpUI _levelUpUI;
    [SerializeField] private UnitLimitBreakUI _limitBreakUI;
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



    public StageSelectUI StageSelectUI { get { return _stageUI; } }
    public UnitListUI UnitListUI { get {return _unitListUI; } }
    public CharacterInfoUI CharacterInfoUI { get { return _characterInfoUI; } }
    public UnitLevelUpUI LevelUpUI { get { return _levelUpUI; } }
    public UnitLimitBreakUI LimitBreakUI {  get { return _limitBreakUI; } }
    private void Start()
    {
       
        Instance = this;
        Initialized();
        Battle.onClick.AddListener(() =>
        {
            StageSelectUI.gameObject.SetActive(true);
        });
        Character.onClick.AddListener(() =>
        {
           _unitListUI.gameObject.SetActive(true);
        });
    }
    private void Initialized() {
        _stageUI.gameObject.SetActive(false);
        _unitListUI.gameObject.SetActive(false);
        _characterInfoUI.gameObject.SetActive(false);
        _levelUpUI.gameObject.SetActive(false);
        _limitBreakUI.gameObject.SetActive(false);
    }
}
