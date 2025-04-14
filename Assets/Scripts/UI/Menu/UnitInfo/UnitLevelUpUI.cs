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
    [SerializeField] private Button LevelUpBtn;
    [SerializeField] private Button IncreaseBtn;
    [SerializeField] private Button DecreaseBtn;
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private TextMeshProUGUI TargetLevel;
    [SerializeField] private CharacterInfoUI characterInfo;

    public void Initialized(AllianceUnit allanceUnit)
    {
        unit = allanceUnit;
    }
    private void Start()
    {

        IncreaseBtn.onClick.AddListener(() =>
        {

            if ((CurrentViewLevel >= unit.Rarity.LevelCap[CurrentLimtBreak])) return;

            SetTargetLevel(CurrentViewLevel + 1);
            characterInfo.PreviewStat(CurrentLimtBreak);
        });
        DecreaseBtn.onClick.AddListener(() =>
        {
            if ((CurrentViewLevel <= characterInfo.GetCurrentLevel())) return;


            SetTargetLevel(CurrentViewLevel - 1);
            characterInfo.PreviewStat(CurrentLimtBreak);

        });
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
        characterInfo.PreviewStat(CurrentLimtBreak);
        


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
    private bool MaxLevelCheck(int lb)
    {
        if (characterInfo.GetCurrentLevel() == unit.Rarity.LevelCap[lb]) return true;
        return false;

    }
}
