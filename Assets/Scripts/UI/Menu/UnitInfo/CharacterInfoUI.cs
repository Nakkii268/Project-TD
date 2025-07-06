using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoUI : UICanvas
{
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private AllianceUnit unit1;
    //

    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;



    //init
    [Header("Stat and Info")]
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

    [SerializeField] private Image LimitBreakIcon;
    [SerializeField] private Image UnitSplashArt;
    [SerializeField] private Image UnitRarityIcon;
    [SerializeField] private Image AttackRangeVisualized;
    [SerializeField] private Image UnitClassIcon;
    //
    [SerializeField] private PlayerData PlayerData;

    //skill detail
    [Header("Skills")]
    [SerializeField] private Image Skill1Sprite;
    [SerializeField] private Image Skill2Sprite;
    [SerializeField] private Button Skill1Detail;
    [SerializeField] private Button Skill2Detail;
    [SerializeField] private TextMeshProUGUI SkillName;
    [SerializeField] private TextMeshProUGUI SkillDesc;
    [SerializeField] private TextMeshProUGUI SkillTypeTxt;
    [SerializeField] private TextMeshProUGUI SkillSP;
    [SerializeField] private TextMeshProUGUI ActiveType;
    [SerializeField] private Transform SkillInfo; //container
    [SerializeField] private Button SkillInfoBtn; //container
    [SerializeField] private ContentFitter Fitter;
    [SerializeField] private RectTransform LayoutGroup;
 
    public void Initialized(AllianceUnit allanceUnit)
    {
        unit = allanceUnit;
        UpdateUI(allanceUnit);

        

    }
    public override void SetUp(AllianceUnit unit)
    {
        Initialized(unit);
    }
    private void Start()
    {
        if (unit == null) return; //first time load, incase error happened


        GameManager.Instance._playerDataManager.OnDataChange += _playerDataManager_OnDataChange;
        LevelUpBtn.onClick.AddListener(() => {
            UIManager.Instance.OpenUI<UnitLevelUpUI>(unit);
           // unit.Level = 80;

        });
        LimitBreakBtn.onClick.AddListener(() => {

            UIManager.Instance.OpenUI<UnitLimitBreakUI>(unit);

        });

        BackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close<CharacterInfoUI>(0);
        });
        HomeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ToHomeMenu();
        });
        SkillInfoBtn.onClick.AddListener(() =>
        {
            UpdateSkillDetail(0);
            SkillInfo.gameObject.SetActive(!SkillInfo.gameObject.activeSelf);
            Fitter.UpdateSize();
            LayoutRebuilder.ForceRebuildLayoutImmediate(LayoutGroup);
           
        } );
    }

    private void _playerDataManager_OnDataChange(object sender, EventArgs e)
    {
        UpdateUI(unit);
       
    }

    private void OnDisable()
    {
        GameManager.Instance._playerDataManager.OnDataChange -= _playerDataManager_OnDataChange;

    }
    private void SetLimtBreakText(int v)
    {
       LimitBreakIcon.sprite = GameManager.Instance.limitBreakIcon.GetIcon(v);
    }
    private void SetLevelText(int v)
    {
       LevelText.text = v.ToString();
    }

 


 


    public bool IsMaxLevel(int lb)
    {
        if (unit.Level == unit.Rarity.LevelCap[lb]) return true;
        return false;

    }
    public bool IsMaxLimitBreak()
    {
        if(unit.LimitBreak >= unit.Rarity.MaxLimitBreak) return true;
        return false;
    }

    private void UpdateUI(AllianceUnit allanceUnit)
    {
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
        if (IsMaxLevel(unit.LimitBreak))
        {

            LevelUpBtn.interactable = false;
        }
        else
        {
            LevelUpBtn.interactable = true;

        }
        if (IsMaxLimitBreak())
        {
            Debug.Log(unit.Name + "max");
            LimitBreakBtn.interactable = false;
        }
        else
        {
            Debug.Log(unit.Name + "NOT MAX  ");

            LimitBreakBtn.interactable = true;

        }
        Skill1Sprite.sprite  = allanceUnit.UnitSkills[0].Icon;
        Skill2Sprite.sprite  = allanceUnit.UnitSkills[1].Icon;
        Skill1Detail.onClick.AddListener(() =>
        {
            UpdateSkillDetail(0);
           
        });
        Skill2Detail.onClick.AddListener(() =>
        {
            UpdateSkillDetail(1);

            
        });

    }
 
    private void UpdateSkillDetail(int index)
    {
        if(index == 0)
        {
            SkillName.text = unit.UnitSkills[0].Name;
            SkillDesc.text = unit.UnitSkills[0].Description;
            SkillTypeTxt.text = unit.UnitSkills[0].skillType.ToString();
            if (unit.UnitSkills[0].skillType == SkillType.Passive)
            {
                ActiveType.text = "Auto";
                SkillSP.text = "0";
            }
            else
            {
                ActiveSkills skill = (ActiveSkills)unit.UnitSkills[0];
                ActiveType.text = skill.ActiveType.ToString();
                SkillSP.text = skill.SkillPoint.ToString();
            }
        }
        else
        {
            SkillName.text = unit.UnitSkills[1].Name;
            SkillDesc.text = unit.UnitSkills[1].Description;
            SkillTypeTxt.text = unit.UnitSkills[1].skillType.ToString();
            if (unit.UnitSkills[1].skillType == SkillType.Passive)
            {
                ActiveType.text = "Auto";
                SkillSP.text = "0";
            }
            else
            {
                ActiveSkills skill = (ActiveSkills)unit.UnitSkills[1];
                ActiveType.text = skill.ActiveType.ToString();
                SkillSP.text = skill.SkillPoint.ToString();
            }
        }

    }

 

}
