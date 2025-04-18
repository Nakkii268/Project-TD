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
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
    
    [SerializeField] private int CurrentViewLevel;
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
    [SerializeField] private TextMeshProUGUI LimitBreakText;
    [SerializeField] private UnitLevelUpUI LevelUpUI;
    [SerializeField] private UnitLimitBreakUI LimitBreakUI;
    [SerializeField] private Image UnitSplashArt;
    [SerializeField] private Image UnitRarityIcon;
    [SerializeField] private Image AttackRangeVisualized;
    [SerializeField] private Image UnitClassIcon;
    //
    [SerializeField] private PlayerData PlayerData;


    public void Initialized(AllianceUnit allanceUnit)
    {
        unit = allanceUnit;
        UnitName.text = unit.Name.ToString();
        UnitAttack.text = unit.Attack.ToString();
        UnitHp.text = unit.Heath.ToString();
        UnitDef.text = unit.Defense.ToString();
        UnitRes.text = unit.Resistance.ToString();
        UnitAS.text = unit.AttackInterval.ToString();
        UnitBlock.text = unit.Block.ToString();
        UnitCost.text = unit.UnitDp.ToString();
        UnitRedeploy.text = unit.RedeployTime.ToString();
        UnitSplashArt.sprite = unit.unitSplashArt;
        UnitRarityIcon.sprite = unit.Rarity.RarityIcon;
        UnitClassIcon.sprite = unit.UnitClass.ClassIcon;
        AttackRangeVisualized.sprite = unit.UnitRangeVisualized;
        
    }
    private void Start()
    {
        //PlayerPrefs.SetInt(unit.UnitID, 1);
       // PlayerPrefs.SetInt(unit.UnitID + "lb", 0);
        
       /* SetLimtBreakText(GetCurrentLimitBreak());
        SetLevelText(GetCurrentLevel());
        BackBtn.onClick.AddListener(() =>
        {
            Debug.Log("Back");
        });
        HomeBtn.onClick.AddListener(() => {
            Debug.Log("home");
        });
        LevelUpBtn.onClick.AddListener(() =>
        {
            LevelUpUI.gameObject.SetActive(true);
            LevelUpUI.Initialized(unit);
        });
        LimitBreakBtn.onClick.AddListener(() =>
        {
            LimitBreakUI.gameObject.SetActive(true);
            LimitBreakUI.Initialized(unit);

        });
       
        */
        Initialized(unit1);
        

    }

    private void SetLimtBreakText(int v)
    {
       LimitBreakText.text = v.ToString();
    }
    private void SetLevelText(int v)
    {
       LevelText.text = v.ToString();
    }

    public void PreviewStat(int lb)
    {
        float currentAttack = 0;
        float currentHealth = 0;
        float currentDef = 0;

        if (CurrentLimtBreak > 0)
        {
            for (int i = 0; i <= CurrentLimtBreak - 1; i++)
            {
               
                currentAttack += (unit.UnitClass.ClassLevelUpData.data[i].StatBonus[0].StatModifier * (unit.Rarity.LevelCap[i] - 1));

                currentHealth += (unit.UnitClass.ClassLevelUpData.data[i].StatBonus[1].StatModifier * (unit.Rarity.LevelCap[i] - 1));

                currentDef += (unit.UnitClass.ClassLevelUpData.data[i].StatBonus[2].StatModifier * (unit.Rarity.LevelCap[i] - 1));

            }
        }
        UnitAttack.text = (currentAttack + (unit.Attack + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[0].StatModifier * GetCurrentLevel() - 1)).ToString()
            + " +" + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[0].StatModifier * (CurrentViewLevel - GetCurrentLevel())).ToString();

        UnitHp.text = (currentHealth + (unit.Heath + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[1].StatModifier * GetCurrentLevel() - 1)).ToString() +
            " +" + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[1].StatModifier * (CurrentViewLevel - GetCurrentLevel())).ToString();

        UnitDef.text = (currentDef + (unit.Defense + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[2].StatModifier * GetCurrentLevel() - 1)).ToString() +
            "+ " + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[2].StatModifier * (CurrentViewLevel - GetCurrentLevel())).ToString();
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
   
 
 
  
    
    
   
}
