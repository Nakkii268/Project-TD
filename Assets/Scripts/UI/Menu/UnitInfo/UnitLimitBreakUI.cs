using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class UnitLimitBreakUI : UICanvas
{
    [SerializeField] private AllianceUnit unit;
  
    [SerializeField] private int CurrentLimtBreak;
    [SerializeField] private Image CurrentLBIcon;
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
    [SerializeField] private Button LimitBreakBtn;
    [SerializeField] private Button CancelBtn;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject ItemRequiredPrefab;
    [SerializeField] private Animator Animator;
    
   
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
        CurrentLBIcon.sprite = GameManager.Instance.limitBreakIcon.GetIcon(CurrentLimtBreak);
       
        if (!RequiredCheck())
        {
            LimitBreakBtn.interactable = false;
        }
        else
        {
            LimitBreakBtn.interactable = true;

        }
        
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
                ConsumeMaterial();
                LimitBreak();
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
      
        GameManager.Instance._playerDataManager.PlayerDataSO.UpdateUnit(unit);
        Animator.Play("LimitBreak");
        StartCoroutine(Delay(.4f));
        CurrentLBIcon.sprite = GameManager.Instance.limitBreakIcon.GetIcon(CurrentLimtBreak);



    }
    private bool RequiredCheck()
    {
        if (unit.Level < unit.Rarity.LevelCap[unit.LimitBreak])
        {
        
            return false;
        }
        List<ItemsData> items = unit.UnitClass.ClassLimitBreakpData.LBData[unit.LimitBreak].MaterialsRequired;
        for (int i = 0;i< items.Count; i++)
        {
            if (GameManager.Instance._playerDataManager.PlayerDataSO.IsHaveItem(items[i].Item.ItemID) < items[i].Quantity) {
                Debug.Log(GameManager.Instance._playerDataManager.PlayerDataSO.IsHaveItem(items[i].Item.ItemID) + "qtt");
                Debug.Log(items[i].Quantity + "rq");
                return false; 
            }
            
            
        }
        return true;
        
    }
   


    private void ConsumeMaterial()
    {
        List<ItemsData> items = unit.UnitClass.ClassLimitBreakpData.LBData[unit.LimitBreak].MaterialsRequired;
        for (int i = 0; i < items.Count; i++)
        {

            GameManager.Instance._playerDataManager.PlayerDataSO.RemoveItem(items[i].Item.ItemID, items[i].Quantity);
            
        }
    }

    private IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
