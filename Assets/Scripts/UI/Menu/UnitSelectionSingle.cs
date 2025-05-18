using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelectionSingle : MonoBehaviour
{
    [SerializeField] private AllianceUnit _unit;
    [SerializeField] private Button _btn;
    [SerializeField] private int CurIndex;
    
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image ClassIcon;
    [SerializeField] private Image RarityIcon;
    [SerializeField] private Image UIPotrait;
    [SerializeField] private GameObject Selected;

    public event EventHandler<UnitSelectArg> OnUnitSelected;
    [SerializeField] private bool isSelected;

    public void Initialized(AllianceUnit unit,int index)
    {
        
        _unit = unit;
        _nameText.text = unit.Name;
        _levelText.text = unit.Level.ToString();
        ClassIcon.sprite = unit.UnitClass.ClassIcon;
        RarityIcon.sprite = unit.Rarity.RarityIcon;
        UIPotrait.sprite = unit.unitUIPotrait;
        CurIndex = index;
        Selected.SetActive(false);
       

    }

    private void Start()
    {

        _btn.onClick.AddListener(() =>
        {
            if (!isSelected) {
                isSelected = true;
                OnUnitSelected?.Invoke(this, new UnitSelectArg(_unit,CurIndex));
                Selected.SetActive(true);
            }
            else
            {
                isSelected = false;
                OnUnitSelected?.Invoke(this, new UnitSelectArg(null, -1));

                Selected.SetActive(false);

            }

        });
        
    }
    public void UnSelected()
    {
        isSelected=false;
        Selected.SetActive(false);
    }
}
public class UnitSelectArg
{
    public AllianceUnit Unit;
    public int Index;
    public UnitSelectArg(AllianceUnit unit, int index) { 
        Unit = unit;
        Index = index;
    }
}
