using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
    [SerializeField] private Button LevelUpBtn;
    [SerializeField] private Button LimitBreakBtn;
    [SerializeField] private Button IncreaseBtn;
    [SerializeField] private Button DecreaseBtn;
    [SerializeField] private TextMeshProUGUI TargetLevel;
    [SerializeField] private int CurrentViewLevel;
    [SerializeField] private int CurrentLimtBreak;
    [SerializeField] private TextMeshProUGUI LimitBreakText;
    [SerializeField] private TextMeshProUGUI UnitAttack;
    [SerializeField] private TextMeshProUGUI UnitHp;
    [SerializeField] private TextMeshProUGUI UnitDef;
    [SerializeField] private Transform LevelUpUI;
    [SerializeField] private Transform LimitBreakUI;
    [SerializeField] private PlayerData PlayerData;
    
    private void Start()
    {
        PlayerPrefs.SetInt(unit.UnitID, 1);
        PlayerPrefs.SetInt(unit.UnitID + "lb", 0);
        SetTargetLevel(GetCurrentLevel());
        SetTargetLimtBreak(GetCurrentLimitBreak());
        BackBtn.onClick.AddListener(() =>
        {
            Debug.Log("Back");
        });
        HomeBtn.onClick.AddListener(() => {
            Debug.Log("home");
        });
        LevelUpBtn.onClick.AddListener(() =>
        {
            LevelUp();
        });
        LimitBreakBtn.onClick.AddListener(() =>
        {
            LimitBreak();
        });
        IncreaseBtn.onClick.AddListener(() =>
        {

            if ((CurrentViewLevel >= unit.Rarity.LevelCap[CurrentLimtBreak])) return;
            
            SetTargetLevel(CurrentViewLevel + 1);
            PreviewStat(CurrentLimtBreak);
        });
        DecreaseBtn.onClick.AddListener(() =>
        {
            if ((CurrentViewLevel <= GetCurrentLevel())) return;


            SetTargetLevel(CurrentViewLevel -1);
            PreviewStat(CurrentLimtBreak);

        });

        
        UIManage();

    }
    private void PreviewStat(int lb)
    {
        float currentAttack = 0;
        float currentHealth = 0;
        float currentDef = 0;
        
        if (CurrentLimtBreak > 0)
        {
            for (int i = 0; i <= CurrentLimtBreak-1 ; i++)
            {
                Debug.Log(i);
                currentAttack += ( unit.UnitClass.ClassLevelUpData.data[i].StatBonus[0].StatModifier * (unit.Rarity.LevelCap[i] - 1));
          
                currentHealth += ( unit.UnitClass.ClassLevelUpData.data[i].StatBonus[1].StatModifier * (unit.Rarity.LevelCap[i] - 1));

                currentDef += ( unit.UnitClass.ClassLevelUpData.data[i].StatBonus[2].StatModifier * (unit.Rarity.LevelCap[i] - 1));
               
            }
        }
        UnitAttack.text = (currentAttack+ (unit.Attack + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[0].StatModifier * GetCurrentLevel()-1)).ToString() + " +" + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[0].StatModifier * (CurrentViewLevel - GetCurrentLevel())).ToString();
        UnitHp.text = (currentHealth+ (unit.Heath + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[1].StatModifier * GetCurrentLevel()-1)).ToString() + " +" + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[1].StatModifier * (CurrentViewLevel - GetCurrentLevel())).ToString();
        UnitDef.text = (currentDef+(unit.Defense + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[2].StatModifier * GetCurrentLevel()-1)).ToString() + "+ " + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[2].StatModifier * (CurrentViewLevel - GetCurrentLevel())).ToString();
    }
    private int GetCurrentLevel()
    {
        if (!PlayerPrefs.HasKey(unit.UnitID)) return 1;
       return PlayerPrefs.GetInt(unit.UnitID);
    }
    private int GetCurrentLimitBreak()
    {
        if (!PlayerPrefs.HasKey(unit.UnitID + "lb")) return 0;
        return PlayerPrefs.GetInt(unit.UnitID + "lb");
    }
    private void SetTargetLevel(int level)
    {
        CurrentViewLevel = level;
        TargetLevel.text = level.ToString();
    }
    private void SetTargetLimtBreak(int lb)
    {
        CurrentLimtBreak=lb; 
        LimitBreakText.text = lb.ToString();
    }
 
    private void LevelUp()
    {
       /* if((PlayerData.Exp < unit.UnitClass.ClassLevelUpData.data[CurrentLimtBreak].CurrencyRequired[0].Value)
            &&(PlayerData.Gold < unit.UnitClass.ClassLevelUpData.data[CurrentLimtBreak].CurrencyRequired[1].Value))
        {
            return;
        }*/
        //if not enough material then erorr ( test later)
        PlayerPrefs.SetInt(unit.UnitID, CurrentViewLevel);
        PreviewStat(CurrentLimtBreak);
        UIManage();
        

    }
    private void LimitBreak()
    {
        if (GetCurrentLevel() < unit.Rarity.LevelCap[CurrentLimtBreak]) return;
        if (CurrentLimtBreak == 2) return;
        PlayerPrefs.SetInt(unit.UnitID+"lb",CurrentLimtBreak+1);
        SetTargetLimtBreak(CurrentLimtBreak + 1);
        SetTargetLevel(1);
        PlayerPrefs.SetInt(unit.UnitID ,  1);
        PreviewStat(CurrentLimtBreak);
        UIManage();

    }
    private bool LevelUpCheck(int lb)
    {
        if ((CurrentViewLevel <= GetCurrentLevel())||(CurrentViewLevel >= unit.Rarity.LevelCap[lb]))
        {
            return false;
        }
        return true;
    }
    private bool MaxLevelCheck(int lb)
    {
        if (GetCurrentLevel() == unit.Rarity.LevelCap[lb]) return true;
        return false;
        
    }
    private void UIManage()
    {
        if (MaxLevelCheck(CurrentLimtBreak) && CurrentLimtBreak != 2)
        {

            LevelUpUI.gameObject.SetActive(false);
            LimitBreakUI.gameObject.SetActive(true);

        }
        else 
        {

            LevelUpUI.gameObject.SetActive(true);
            LimitBreakUI.gameObject.SetActive(false);

        }
    }
}
