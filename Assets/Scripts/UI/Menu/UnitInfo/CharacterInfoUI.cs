using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : MonoBehaviour
{
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private AllianceUnit unit1;
    //

    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
    
    
    [SerializeField] private int CurrentLimtBreak;
    //init
    [SerializeField] private Button LevelUpBtn;
    [SerializeField] private Button LimitBreakBtn;
    [SerializeField] private TextMeshProUGUI UnitName;

    [SerializeField] private TextMeshProUGUI UnitAttack;
    [SerializeField] private TextMeshProUGUI UnitHp;
    [SerializeField] private TextMeshProUGUI UnitDef;
    [SerializeField] private TextMeshProUGUI UnitRes;
    [SerializeField] private TextMeshProUGUI UnitAS;
    [SerializeField] private TextMeshProUGUI UnitBlock;
    [SerializeField] private TextMeshProUGUI UnitRedeploy;
    [SerializeField] private TextMeshProUGUI UnitCost;
    [SerializeField] private TextMeshProUGUI LevelText;
    [SerializeField] private UnitLevelUpUI LevelUpUI;
    [SerializeField] private UnitLimitBreakUI LimitBreakUI;
    [SerializeField] private Image LimitBreakIcon;
    [SerializeField] private Image UnitSplashArt;
    [SerializeField] private Image UnitRarityIcon;
    [SerializeField] private Image AttackRangeVisualized;
    [SerializeField] private Image UnitClassIcon;
    //
    [SerializeField] private PlayerData PlayerData;


    public void Initialized(AllianceUnit allanceUnit)
    {
        unit = allanceUnit;
        UnitName.text = allanceUnit.Name.ToString();
        UnitAttack.text = allanceUnit.Attack.ToString();
        UnitHp.text = allanceUnit.Heath.ToString();
        UnitDef.text = allanceUnit.Defense.ToString();
        UnitRes.text = allanceUnit.Resistance.ToString();
        UnitAS.text = allanceUnit.AttackInterval.ToString();
        UnitBlock.text = allanceUnit.Block.ToString();
        UnitCost.text = allanceUnit.UnitDp.ToString();
        UnitRedeploy.text = allanceUnit.RedeployTime.ToString();
        UnitSplashArt.sprite = allanceUnit.unitSplashArt;
        UnitRarityIcon.sprite = allanceUnit.Rarity.RarityIcon;
        UnitClassIcon.sprite = allanceUnit.UnitClass.ClassIcon;
        AttackRangeVisualized.sprite = allanceUnit.UnitRangeVisualized;
        SetLevelText(allanceUnit.Level);
        SetLimtBreakText(allanceUnit.LimitBreak);
        LevelUpUI = MenuUIManager.Instance.LevelUpUI;
        LimitBreakUI = MenuUIManager.Instance.LimitBreakUI;
        
    }
    private void Start()
    {
        
        //Initialized(unit1);
        if (MaxLevelCheck(CurrentLimtBreak))
        {
            LevelUpBtn.interactable= false;
        }
        if (MaxLimitBreakCheck())
        {
            LimitBreakBtn.interactable= false;
        }

        LevelUpBtn.onClick.AddListener(() => { 

            LevelUpUI.gameObject.SetActive(true); 
        });
        LimitBreakBtn.onClick.AddListener(() => { 

            LimitBreakUI.gameObject.SetActive(true); 
        });

        BackBtn.onClick.AddListener(CloseUI);
        HomeBtn.onClick.AddListener(GoToHome);
    }

    private void OnDisable()
    {
        BackBtn.onClick.RemoveListener(CloseUI);
        HomeBtn.onClick.RemoveListener(GoToHome);
    }
    private void SetLimtBreakText(int v)
    {
       LimitBreakIcon.sprite = GameManager.Instance.limitBreakIcon.GetIcon(v);
    }
    private void SetLevelText(int v)
    {
       LevelText.text = v.ToString();
    }

  
    public void StatShow(int lb)
    {
        float currentAttack = 0;
        float currentHealth = 0;
        float currentDef = 0;

        if (CurrentLimtBreak > 0)
        {
            for (int i = 0; i <= CurrentLimtBreak - 1; i++)
            {
               
                currentAttack += (unit.UnitClass.ClassLevelUpData.data[i].StatBonus[0].StatModify * (unit.Rarity.LevelCap[i] - 1));

                currentHealth += (unit.UnitClass.ClassLevelUpData.data[i].StatBonus[1].StatModify * (unit.Rarity.LevelCap[i] - 1));

                currentDef += (unit.UnitClass.ClassLevelUpData.data[i].StatBonus[2].StatModify * (unit.Rarity.LevelCap[i] - 1));

            }
        }
        UnitAttack.text = (currentAttack + (unit.Attack + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[0].StatModify * GetCurrentLevel() - 1)).ToString();
           
        UnitHp.text = (currentHealth + (unit.Heath + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[1].StatModify * GetCurrentLevel() - 1)).ToString();


        UnitDef.text = (currentDef + (unit.Defense + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[2].StatModify * GetCurrentLevel() - 1)).ToString();
           
    }
    public int GetCurrentLevel()
    {
        if (!PlayerPrefs.HasKey(unit.UnitID)) return 1;
        return PlayerPrefs.GetInt(unit.UnitID);
    }

    public int GetCurrentLimitBreak()
    {
        if (!PlayerPrefs.HasKey(unit.UnitID + "lb")) return 0;
        return PlayerPrefs.GetInt(unit.UnitID + "lb");
    }


    public bool MaxLevelCheck(int lb)
    {
        if (GetCurrentLevel() == unit.Rarity.LevelCap[lb]) return true;
        return false;

    }
    public bool MaxLimitBreakCheck()
    {
        if(GetCurrentLimitBreak()>=unit.Rarity.MaxLimitBreak) return true;
        return false;
    }


    public void CloseUI()
    {
        this.gameObject.SetActive(false);
    }
    public void GoToHome()
    {
        this.gameObject.SetActive(false);

        MenuUIManager.Instance.gameObject.SetActive(true);
    }


}
