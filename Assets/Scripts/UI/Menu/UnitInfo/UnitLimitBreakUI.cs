using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitLimitBreakUI : MonoBehaviour
{
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private CharacterInfoUI characterInfo;
    [SerializeField] private int CurrentLimtBreak;
    [SerializeField] private TextMeshProUGUI LimitBreakText;

    public void Initialized(AllianceUnit allanceUnit)
    {
        unit = allanceUnit;
    }
    private void LimitBreak()
    {
        if (characterInfo.GetCurrentLevel() < unit.Rarity.LevelCap[CurrentLimtBreak]) return;
        if (CurrentLimtBreak == 2) return;
        PlayerPrefs.SetInt(unit.UnitID + "lb", CurrentLimtBreak + 1);
        SetTargetLimtBreak(CurrentLimtBreak + 1);
        //SetTargetLevel(1);
        PlayerPrefs.SetInt(unit.UnitID, 1);
        characterInfo.PreviewStat(CurrentLimtBreak);


    }
    private void SetTargetLimtBreak(int lb)
    {
        CurrentLimtBreak = lb;
        LimitBreakText.text = lb.ToString();
    }
}
