using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitLevelUpUI : UICanvas
{
    [SerializeField] private TextMeshProUGUI UnitAttack;
    [SerializeField] private TextMeshProUGUI UnitHp;
    [SerializeField] private TextMeshProUGUI UnitDef;
    [SerializeField] private int CurrentViewLevel;
    [SerializeField] private int CurrentLimtBreak;

    //btn
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
    [SerializeField] private Button LevelUpBtn;
    [SerializeField] private Button CancelBtn;
    [SerializeField] private Button IncreaseBtn;
    [SerializeField] private Button DecreaseBtn;

    //btn
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private TextMeshProUGUI TargetLevel;
    
    [SerializeField] private Image LevelUpProgress;
    [SerializeField] private float _atk;
    [SerializeField] private float _hp;
    [SerializeField] private float _def;
    public void Initialized(AllianceUnit allanceUnit)
    {
        unit = allanceUnit;
        CurrentViewLevel = GetCurrentLevel();
        CurrentLimtBreak =  GetCurrentLimitBreak();
        SetTargetLevel(CurrentViewLevel);
        PreviewStat(CurrentLimtBreak);
    }
    public override void SetUp(AllianceUnit unit)
    {
        Initialized(unit);
    }
    private void Start()
    {

        UpdateLevelUpProgress(GetCurrentLevel(), unit.Rarity.LevelCap[CurrentLimtBreak]);
        IncreaseBtn.onClick.AddListener(() =>
        {

            if ((CurrentViewLevel >= unit.Rarity.LevelCap[CurrentLimtBreak])) return;

            SetTargetLevel(CurrentViewLevel + 1);
            PreviewStat(CurrentLimtBreak);
            UpdateLevelUpProgress(CurrentViewLevel, unit.Rarity.LevelCap[CurrentLimtBreak]);

        });
        DecreaseBtn.onClick.AddListener(() =>
        {
            if ((CurrentViewLevel <= GetCurrentLevel())) return;


            SetTargetLevel(CurrentViewLevel - 1);
            PreviewStat(CurrentLimtBreak);
            UpdateLevelUpProgress(CurrentViewLevel, unit.Rarity.LevelCap[CurrentLimtBreak]);

        });

        LevelUpBtn.onClick.AddListener(() =>
        {
            LevelUp();
            Debug.Log(GetCurrentLevel());  
        });
        CancelBtn.onClick.AddListener(() =>{
            UIManager.Instance.Close<UnitLevelUpUI>(0);
            UIManager.Instance.OpenUI<CharacterInfoUI>(unit);
        });
        BackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close<UnitLevelUpUI>(0);
            UIManager.Instance.OpenUI<CharacterInfoUI>(unit);

        });
        HomeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ToHomeMenu();
        });
    }

    /////////////currenly usin test data. change later /////////
    private void LevelUp()
    {
        //if((GameManager.Instance.testData.Exp < unit.UnitClass.ClassLevelUpData.data[CurrentLimtBreak].CurrencyRequired[0].Value)
          //   &&(GameManager.Instance.testData.Gold < unit.UnitClass.ClassLevelUpData.data[CurrentLimtBreak].CurrencyRequired[1].Value))
        // {
            unit.Level = CurrentViewLevel;
            unit.Attack = _atk;
            unit.Heath = _hp;
            unit.Defense = _def;
            // return;
        // }
        //if not enough material then erorr ( test later)
       // PlayerPrefs.SetInt(unit.UnitID, CurrentViewLevel);
        PreviewStat(CurrentLimtBreak);
        


    }
    private void SetTargetLevel(int level)
    {
        CurrentViewLevel = level;
        TargetLevel.text = level.ToString();
    }

    
    private bool LevelUpCheck(int lb)
    {
        if ((CurrentViewLevel <= GetCurrentLevel()) || (CurrentViewLevel >= unit.Rarity.LevelCap[lb]))
        {
            return false;
        }
        return true;
    }
    
    
    private void UpdateLevelUpProgress(int cur, int max)
    {
        float curf = cur;
        float maxf = max;
        LevelUpProgress.fillAmount = curf/maxf;
    }

    private void ButtonHandle()
    {
        if (MaxLevelCheck(CurrentLimtBreak))
        {
            LevelUpBtn.interactable = false;
            IncreaseBtn.interactable = false;
            DecreaseBtn.interactable = false;
        }
    }
    public void PreviewStat(int lb)
    {
        
        UnitAttack.text = unit.Attack .ToString() 
            + " +" + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[0].StatModify * (CurrentViewLevel - GetCurrentLevel())).ToString();

        //atk
        _atk = unit.Attack
            + 
            (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[0].StatModify * (CurrentViewLevel - GetCurrentLevel()));

        UnitHp.text = unit.Heath.ToString() +
            " +" + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[1].StatModify * (CurrentViewLevel - GetCurrentLevel())).ToString();

        //hp
        _hp = unit.Heath 
            + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[1].StatModify * (CurrentViewLevel - GetCurrentLevel()));


        UnitDef.text = unit.Defense.ToString() +
            "+ " + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[2].StatModify * (CurrentViewLevel - GetCurrentLevel())).ToString();

        //def
        _def = unit.Defense 
            + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[2].StatModify * (CurrentViewLevel - GetCurrentLevel()));
    }

    public int GetCurrentLevel()
    {

        return unit.Level;
    }

    public int GetCurrentLimitBreak()
    {
        
        return unit.LimitBreak;
    }
    public bool MaxLevelCheck(int lb)
    {
        if (GetCurrentLevel() == unit.Rarity.LevelCap[lb]) return true;
        return false;

    }
}
