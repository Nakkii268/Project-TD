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

    [SerializeField] private int Index;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image ClassIcon;
    [SerializeField] private Image RarityIcon;
    [SerializeField] private Image UIPotrait;
    [SerializeField] private GameObject Selected;

    public event EventHandler<LineUpSave> OnUnitSelected;
    [SerializeField] public bool isSelected;
    [SerializeField] public bool isInteractable;

    public void Initialized(AllianceUnit unit,int i,AllianceUnit from=null)
    {
        
        _unit = unit;
        _nameText.text = unit.Name;
        _levelText.text = unit.Level.ToString();
        ClassIcon.sprite = unit.UnitClass.ClassIcon;
        RarityIcon.sprite = unit.Rarity.RarityIcon;
        UIPotrait.sprite = unit.unitUIPotrait;
        Index = i;
        Selected.SetActive(false);
        if (from == unit) { 
            isSelected = true;
            Selected.SetActive(true);
            isInteractable = true;
            OnUnitSelected?.Invoke(this, new LineUpSave(i, unit));

        }


    }

    private void Start()
    {

        _btn.onClick.AddListener(() =>
        {
            
            Debug.Log(isSelected);
           if (!isSelected) {
                isSelected = !isSelected;
                Selected.SetActive(true);
                OnUnitSelected?.Invoke(this, new LineUpSave(Index,_unit));
               

            }
            else
            {
                isSelected = !isSelected;
                Selected.SetActive(false);
                OnUnitSelected?.Invoke(this, new LineUpSave(-1, _unit));
                

            }

        });
        if(UIManager.Instance.GetUI<LineUpUI>()._tempSquad.ContainsKey( _unit.UnitID) && !isInteractable)
        {
            _btn.interactable = false;
            
        }
        
    }
    public void UnSelected()
    {
        isSelected=false;
        Selected.SetActive(false);
    }

}

