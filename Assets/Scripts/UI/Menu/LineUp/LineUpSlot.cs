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
    [SerializeField] private Button RemoveBtn;
    [SerializeField] private int SlotIndex;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image ClassIcon;
    [SerializeField] private Image RarityIcon;
    [SerializeField] private Image UIPotrait;
    [SerializeField] private Image NullPotrait;
    public event EventHandler<LineUpSave> OnUnitAssign;
    public void Initialized(AllianceUnit unit)
    {
        if (unit == null)
        {
            RemoveBtn.gameObject.SetActive(false);
            NullPotrait.gameObject.SetActive(true);
            return;
        }
        SlotUnit = unit;
        _nameText.text = unit.Name;
        _levelText.text = unit.Level.ToString();
        ClassIcon.sprite = unit.UnitClass.ClassIcon;
        RarityIcon.sprite = unit.Rarity.RarityIcon;
        UIPotrait.sprite = unit.unitUIPotrait;
        RemoveBtn.gameObject.SetActive(true);
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
            UIManager.Instance.OpenUI<UnitSelectionUI>(SlotUnit);
            UIManager.Instance.GetUI<UnitSelectionUI>().OnUnitConfirm += LineUpSlot_OnUnitConfirm;
            
        });
        RemoveBtn.onClick.AddListener(() =>
        {
            OnUnitAssign?.Invoke(this, new LineUpSave(-1, SlotUnit));
            Initialized(null);
            SlotUnit = null;

        });
    }
    
    private void LineUpSlot_OnUnitConfirm(object sender, LineUpSave e)
    {
        if (e.Index == -1)
        {
            OnUnitAssign?.Invoke(this, new LineUpSave(-1, e.Unit));
            Debug.Log(e.Unit);
            Initialized(null);
            return;

        }
        OnUnitAssign?.Invoke(this, new LineUpSave(SlotIndex, e.Unit));
        Initialized(e.Unit);

    }
}

