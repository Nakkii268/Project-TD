using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitLimitBreakUI : UICanvas
{
    [SerializeField] private AllianceUnit unit;
  
    [SerializeField] private int CurrentLimtBreak;
    [SerializeField] private TextMeshProUGUI LimitBreakText;
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
    public void Initialized(AllianceUnit allanceUnit)
    {
        unit = allanceUnit;
    }
    public override void SetUp(AllianceUnit unit)
    {
        Initialized(unit);
    }
    private void LimitBreak()
    {
        if (GetCurrentLevel() < unit.Rarity.LevelCap[CurrentLimtBreak]) return;
        if (CurrentLimtBreak == 2) return;
        PlayerPrefs.SetInt(unit.UnitID + "lb", CurrentLimtBreak + 1);
        SetTargetLimtBreak(CurrentLimtBreak + 1);
        //SetTargetLevel(1);
        PlayerPrefs.SetInt(unit.UnitID, 1);

        BackBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close<UnitLimitBreakUI>(0);
        });
        HomeBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.ToHomeMenu();
        });


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
}
