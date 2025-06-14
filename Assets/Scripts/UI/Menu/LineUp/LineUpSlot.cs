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
    [SerializeField] private int SkillIndex;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image ClassIcon;
    [SerializeField] private Image RarityIcon;
    [SerializeField] private Image limitBrrakIcon;
    [SerializeField] private Image UIPotrait;
    [SerializeField] private Image NullPotrait;
    [SerializeField] private Image SkillIcon;
    public event EventHandler<LineUpSave> OnUnitAssign;
    public void Initialized(AllianceUnit unit,int skillIndex)
    {
        if (unit == null)
        {
            SlotUnit = null;
            NullPotrait.gameObject.SetActive(true);
            return;
        }
        SlotUnit = unit;
        SkillIndex = skillIndex;
        _nameText.text = unit.Name;
        _levelText.text = unit.Level.ToString();
        ClassIcon.sprite = unit.UnitClass.ClassIcon;
        RarityIcon.sprite = unit.Rarity.RarityIcon;
        UIPotrait.sprite = unit.unitUIPotrait;
        //SkillIcon.sprite= unit.UnitSkills[skillIndex].Icon;///////
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
            UIManager.Instance.OpenUI<UnitSelectionUI>(new SlotData(SlotUnit,SlotIndex, SkillIndex));
            UIManager.Instance.GetUI<UnitSelectionUI>().OnUnitConfirm += LineUpSlot_OnUnitConfirm;
            
        });
        
    }
    
    private void LineUpSlot_OnUnitConfirm(object sender, LineUpSave e)
    {
        Debug.Log("unit confirm");
        if (e.Index == -1)
        {
            OnUnitAssign?.Invoke(this, new LineUpSave(-1, e.Unit,e.SkillIndex));
            
            Initialized(null,-1);
            return;

        }
        OnUnitAssign?.Invoke(this, new LineUpSave(SlotIndex, e.Unit, e.SkillIndex));
        Initialized(e.Unit,e.SkillIndex);

    }
}
public class SlotData
{
    public AllianceUnit unit;
    public int index;
    public int skillIndex;
    public SlotData(AllianceUnit unit, int index,int skillIndex)
    {
        this.unit = unit;
        this.index = index;
        this.skillIndex = skillIndex;
    }
}
