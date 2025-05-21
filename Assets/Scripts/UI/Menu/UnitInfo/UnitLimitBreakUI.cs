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
        for (int i = 0; i < unit.UnitClass.ClassLimitBreakpData.MaterialsRequired.Count; i++)
        {
            GameObject single = Instantiate(ItemRequiredPrefab, container);
            single.GetComponent<MaterialRequiredSingle>().
                Init(unit.UnitClass.ClassLimitBreakpData.MaterialsRequired[i], 
                GetItemQuantity(unit.UnitClass.ClassLimitBreakpData.MaterialsRequired[i].Material.ItemID));
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

        

        


    }
    private bool RequiredCheck()
    {
        List<ItemsData> items = unit.UnitClass.ClassLimitBreakpData.MaterialsRequired;
        for (int i = 0;i< items.Count; i++)
        {
            if (!GameManager.Instance.testData.IsHaveItem(items[i].Material.ItemID)) return false;
            if (GameManager.Instance.testData.GetItem(items[i].Material.ItemID).Quantity >= items[i].Quantity) return true;
            return false;
            
        }
        return false;
        
    }
    private int GetItemQuantity(string itemID)
    {
        if (!GameManager.Instance.testData.IsHaveItem(itemID)) return 0;
        return GameManager.Instance.testData.GetItem(itemID).Quantity;
    }
    private void SetTargetLimtBreak(int lb)
    {
        CurrentLimtBreak = lb;
        LimitBreakText.text = lb.ToString();
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

    private void ConsumeMaterial()
    {
        List<ItemsData> items = unit.UnitClass.ClassLimitBreakpData.MaterialsRequired;
        for (int i = 0; i < items.Count; i++)
        {

            GameManager.Instance.testData.GetItem(items[i].Material.ItemID).Quantity -= items[i].Quantity;
            
        }
    }
}
