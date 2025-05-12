using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitLimitBreakUI : MonoBehaviour
{
    [SerializeField] private AllianceUnit unit;
    [SerializeField] private CharacterInfoUI characterInfo;
    [SerializeField] private int CurrentLimtBreak;
    [SerializeField] private TextMeshProUGUI LimitBreakText;
    [SerializeField] private Button BackBtn;
    [SerializeField] private Button HomeBtn;
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

        BackBtn.onClick.AddListener(CloseUI);
        HomeBtn.onClick.AddListener(GoToHome);


    }
    private void SetTargetLimtBreak(int lb)
    {
        CurrentLimtBreak = lb;
        LimitBreakText.text = lb.ToString();
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
