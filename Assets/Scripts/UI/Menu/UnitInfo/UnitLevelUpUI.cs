using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitLevelUpUI : MonoBehaviour
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
    [SerializeField] private Button IncreaseBtn;
    [SerializeField] private Button DecreaseBtn;

    //btn
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private TextMeshProUGUI TargetLevel;
    [SerializeField] private CharacterInfoUI characterInfo;
    [SerializeField] private Image LevelUpProgress;

    public void Initialized(AllianceUnit allanceUnit)
    {
        unit = allanceUnit;
        CurrentViewLevel = characterInfo.GetCurrentLevel();
        CurrentLimtBreak = characterInfo.GetCurrentLimitBreak();
    }
    private void Start()
    {

        UpdateLevelUpProgress(characterInfo.GetCurrentLevel() , unit.Rarity.LevelCap[CurrentLimtBreak]);
        IncreaseBtn.onClick.AddListener(() =>
        {

            if ((CurrentViewLevel >= unit.Rarity.LevelCap[CurrentLimtBreak])) return;

            SetTargetLevel(CurrentViewLevel + 1);
            PreviewStat(CurrentLimtBreak);
            UpdateLevelUpProgress(CurrentViewLevel, unit.Rarity.LevelCap[CurrentLimtBreak]);

        });
        DecreaseBtn.onClick.AddListener(() =>
        {
            if ((CurrentViewLevel <= characterInfo.GetCurrentLevel())) return;


            SetTargetLevel(CurrentViewLevel - 1);
            PreviewStat(CurrentLimtBreak);
            UpdateLevelUpProgress(CurrentViewLevel ,unit.Rarity.LevelCap[CurrentLimtBreak]);

        });


        BackBtn.onClick.AddListener(CloseUI);
        HomeBtn.onClick.AddListener(GoToHome);
    }
    private void OnDisable()
    {
        BackBtn.onClick.RemoveListener(CloseUI);
        HomeBtn.onClick.RemoveListener(GoToHome);
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
        


    }
    private void SetTargetLevel(int level)
    {
        CurrentViewLevel = level;
        TargetLevel.text = level.ToString();
    }

    
    private bool LevelUpCheck(int lb)
    {
        if ((CurrentViewLevel <= characterInfo.GetCurrentLevel()) || (CurrentViewLevel >= unit.Rarity.LevelCap[lb]))
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
        if (characterInfo.MaxLevelCheck(CurrentLimtBreak))
        {
            LevelUpBtn.interactable = false;
            IncreaseBtn.interactable = false;
            DecreaseBtn.interactable = false;
        }
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


                currentAttack += (unit.UnitClass.ClassLevelUpData.data[i].StatBonus[0].StatModify * (unit.Rarity.LevelCap[i] - 1));

                currentHealth += (unit.UnitClass.ClassLevelUpData.data[i].StatBonus[1].StatModify * (unit.Rarity.LevelCap[i] - 1));

                currentDef += (unit.UnitClass.ClassLevelUpData.data[i].StatBonus[2].StatModify * (unit.Rarity.LevelCap[i] - 1));

            }
        }
        UnitAttack.text = (currentAttack + (unit.Attack + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[0].StatModify * characterInfo.GetCurrentLevel() - 1)).ToString()
            + " +" + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[0].StatModify * (CurrentViewLevel - characterInfo.GetCurrentLevel())).ToString();

        UnitHp.text = (currentHealth + (unit.Heath + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[1].StatModify * characterInfo.GetCurrentLevel() - 1)).ToString() +
            " +" + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[1].StatModify * (CurrentViewLevel - characterInfo.GetCurrentLevel())).ToString();

        UnitDef.text = (currentDef + (unit.Defense + unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[2].StatModify * characterInfo.GetCurrentLevel() - 1)).ToString() +
            "+ " + (unit.UnitClass.ClassLevelUpData.data[lb].StatBonus[2].StatModify * (CurrentViewLevel - characterInfo.GetCurrentLevel())).ToString();
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
