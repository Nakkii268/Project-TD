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
    [SerializeField] private Button IncreaseBtn;
    [SerializeField] private Button DecreaseBtn;
    [SerializeField] private TextMeshProUGUI TargetLevel;
    [SerializeField] private int CurrentViewLevel;
    [SerializeField] private TextMeshProUGUI UnitAttack;
    [SerializeField] private TextMeshProUGUI UnitHp;
    [SerializeField] private TextMeshProUGUI UnitDef;

    private void Start()
    {
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
        IncreaseBtn.onClick.AddListener(() =>
        {
            SetTargetLevel(CurrentViewLevel + 1);
            PreviewStat();
        });
        DecreaseBtn.onClick.AddListener(() =>
        {
            SetTargetLevel(CurrentViewLevel -1);
            PreviewStat();

        });
        SetTargetLevel(GetCurrentLevel());
    }
    private void PreviewStat()
    {
        UnitAttack.text = (unit.Attack + unit.UnitClass.ClassLevelUpData.data[0].StatBonus[0].StatModifier * GetCurrentLevel()).ToString()+" +" + (unit.UnitClass.ClassLevelUpData.data[0].StatBonus[0].StatModifier*(CurrentViewLevel-GetCurrentLevel())).ToString();
        UnitHp.text = (unit.Heath + unit.UnitClass.ClassLevelUpData.data[0].StatBonus[1].StatModifier * GetCurrentLevel()).ToString() + " +" + (unit.UnitClass.ClassLevelUpData.data[0].StatBonus[1].StatModifier*(CurrentViewLevel-GetCurrentLevel())).ToString();
        UnitDef.text = (unit.Defense + unit.UnitClass.ClassLevelUpData.data[0].StatBonus[2].StatModifier * GetCurrentLevel()).ToString() + "+ " + (unit.UnitClass.ClassLevelUpData.data[0].StatBonus[2].StatModifier*(CurrentViewLevel-GetCurrentLevel())).ToString();
    }
    private int GetCurrentLevel()
    {
        if (!PlayerPrefs.HasKey(unit.UnitID)) return 1;
       return PlayerPrefs.GetInt(unit.UnitID);
    }
    private void SetTargetLevel(int level)
    {
        CurrentViewLevel=level; 
        TargetLevel.text = level.ToString();
    }
    private void LevelUp()
    {
        //if not enough material then erorr ( test later)
        PlayerPrefs.SetInt(unit.UnitID, CurrentViewLevel);
    }
}
