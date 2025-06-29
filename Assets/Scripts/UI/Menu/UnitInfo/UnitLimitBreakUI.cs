using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class UnitLimitBreakUI : UICanvas
{
    [SerializeField] private AllianceUnit unit;
  
    [SerializeField] private int CurrentLimtBreak;
    [SerializeField] private TextMeshProUGUI LimitBreakText;
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
    [SerializeField] private Button LimitBreakBtn;
    [SerializeField] private Button CancelBtn;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject ItemRequiredPrefab;
   
    public void Initialized(AllianceUnit allanceUnit)
    {
        unit = allanceUnit;

        //level req
        GameObject levelReq = Instantiate(ItemRequiredPrefab, container);
        levelReq.GetComponent<MaterialRequiredSingle>().
            InitLevelReq(unit.Rarity.LevelCap[unit.LimitBreak], unit.Level);

        //

        //item req
        for (int i = 0; i < unit.UnitClass.ClassLimitBreakpData.LBData[unit.LimitBreak].MaterialsRequired.Count; i++)
        {
            GameObject single = Instantiate(ItemRequiredPrefab, container);
            single.GetComponent<MaterialRequiredSingle>().
                Init(unit.UnitClass.ClassLimitBreakpData.LBData[unit.LimitBreak].MaterialsRequired[i],
                GameManager.Instance._playerDataManager.PlayerDataSO.IsHaveItem(unit.UnitClass.ClassLimitBreakpData.LBData[unit.LimitBreak].MaterialsRequired[i].Item.ItemID));
        }

        
        CurrentLimtBreak = unit.LimitBreak;
    }

    private void Start()
    {
        BackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close<UnitLimitBreakUI>(0);
            UIManager.Instance.OpenUI<CharacterInfoUI>(unit);
        });
        HomeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ToHomeMenu();
        });
        LimitBreakBtn.onClick.AddListener(() => {
           
            if (RequiredCheck())
            {
                LimitBreak();
                ConsumeMaterial();
            }
            return;
        });
        CancelBtn.onClick.AddListener(() => {
            UIManager.Instance.Close<UnitLimitBreakUI>(0);
            UIManager.Instance.OpenUI<CharacterInfoUI>(unit);

        });
    }
    public override void SetUp(AllianceUnit unit)
    {
        Initialized(unit);
    }
    private void LimitBreak()
    {
        if (unit.Level < unit.Rarity.LevelCap[CurrentLimtBreak]) return;
        if (CurrentLimtBreak == 2) return;
        
        unit.Level = 1;
        unit.LimitBreak = CurrentLimtBreak + 1;
        SetTargetLimtBreak(CurrentLimtBreak + 1);
        GameManager.Instance._playerDataManager.PlayerDataSO.UpdateUnit(unit);






    }
    private bool RequiredCheck()
    {
        if(unit.Level > unit.Rarity.LevelCap[unit.LimitBreak]) return false;
        List<ItemsData> items = unit.UnitClass.ClassLimitBreakpData.LBData[unit.LimitBreak].MaterialsRequired;
        for (int i = 0;i< items.Count; i++)
        {
            if (GameManager.Instance._playerDataManager.PlayerDataSO.IsHaveItem(items[i].Item.ItemID)==0) return false;
            if (GameManager.Instance._playerDataManager.PlayerDataSO.GetItemById(items[i].Item.ItemID).Quantity >= items[i].Quantity) return true;
            
            
        }
        return false;
        
    }
   
    private void SetTargetLimtBreak(int lb)
    {
        CurrentLimtBreak = lb;
        LimitBreakText.text = lb.ToString();
    }

  

    public int GetCurrentLimitBreak()
    {
        if (!PlayerPrefs.HasKey(unit.UnitID + "lb")) return 0;
        return PlayerPrefs.GetInt(unit.UnitID + "lb");
    }
 

    private void ConsumeMaterial()
    {
        List<ItemsData> items = unit.UnitClass.ClassLimitBreakpData.LBData[unit.LimitBreak].MaterialsRequired;
        for (int i = 0; i < items.Count; i++)
        {

            GameManager.Instance._playerDataManager.PlayerDataSO.RemoveItem(items[i].Item.ItemID, items[i].Quantity);
            
        }
    }
}
