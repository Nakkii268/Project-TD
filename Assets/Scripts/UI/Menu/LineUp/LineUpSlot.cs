using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LineUpSlot : MonoBehaviour
{
    [SerializeField] private AllianceUnit SlotUnit;
    [SerializeField] private Button _btn;
    
    [SerializeField] private int SlotIndex;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image ClassIcon;
    [SerializeField] private Image RarityIcon;
    [SerializeField] private Image limitBrrakIcon;
    [SerializeField] private Image UIPotrait;
    [SerializeField] private Image NullPotrait;
    public event EventHandler<LineUpSave> OnUnitAssign;
    public void Initialized(AllianceUnit unit)
    {
        if (unit == null)
        {
            SlotUnit = null;
            NullPotrait.gameObject.SetActive(true);
            return;
        }
        SlotUnit = unit;
        _nameText.text = unit.Name;
        _levelText.text = unit.Level.ToString();
        ClassIcon.sprite = unit.UnitClass.ClassIcon;
        RarityIcon.sprite = unit.Rarity.RarityIcon;
        UIPotrait.sprite = unit.unitUIPotrait;
        limitBrrakIcon.sprite = GameManager.Instance.limitBreakIcon.GetIcon(unit.LimitBreak);
        NullPotrait.gameObject.SetActive(false);
        
        
    }
    public void IndexAssign(int i)
    {
        SlotIndex = i;
    }
    private void Start()
    {
        _btn.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<UnitSelectionUI>(new SlotData(SlotUnit,SlotIndex));
            UIManager.Instance.GetUI<UnitSelectionUI>().OnUnitConfirm += LineUpSlot_OnUnitConfirm;
            
        });
        
    }
    
    private void LineUpSlot_OnUnitConfirm(object sender, LineUpSave e)
    {
        if (e.Index == -1)
        {
            OnUnitAssign?.Invoke(this, new LineUpSave(-1, e.Unit,e.SkillIndex));
            
            Initialized(null);
            return;

        }
        OnUnitAssign?.Invoke(this, new LineUpSave(SlotIndex, e.Unit, e.SkillIndex));
        Initialized(e.Unit);

    }
}
public class SlotData
{
    public AllianceUnit unit;
    public int index;
    public SlotData(AllianceUnit unit, int index)
    {
        this.unit = unit;
        this.index = index;
    }
}
