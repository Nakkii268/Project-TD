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

    [SerializeField] private int SlotIndex;
    [SerializeField] private int UnitIndex;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image ClassIcon;
    [SerializeField] private Image RarityIcon;
    [SerializeField] private Image UIPotrait;
    [SerializeField] private GameObject Selected;

    public event EventHandler<UnitSelectData> OnUnitSelected;
    [SerializeField] public bool isSelected;
  

    public void Initialized(AllianceUnit unit,int index,int slotIndex,AllianceUnit from=null)
    {
        
        _unit = unit;
        _nameText.text = unit.Name;
        _levelText.text = unit.Level.ToString();
        ClassIcon.sprite = unit.UnitClass.ClassIcon;
        RarityIcon.sprite = unit.Rarity.RarityIcon;
        UIPotrait.sprite = unit.unitUIPotrait;
        this.SlotIndex = slotIndex;
        UnitIndex =index;
        Selected.SetActive(false);

        if (from == unit)
        {
            isSelected = true;
            Selected.SetActive(true);
            
            OnUnitSelected?.Invoke(this, new UnitSelectData(UnitIndex,slotIndex, unit,0));

        }
        else
        {
            if (GameManager.Instance._playerDataManager.PlayerDataSO.IsLineUpContain(unit))
            {
                _btn.interactable = false;
            }

        }
    }

    private void Start()
    {

        _btn.onClick.AddListener(() =>
        {
            
          
           if (!isSelected) {
                isSelected = !isSelected;
                Selected.SetActive(true);
                OnUnitSelected?.Invoke(this, new UnitSelectData(UnitIndex,SlotIndex,_unit, 0));
               

            }
            else
            {
                isSelected = !isSelected;
                Selected.SetActive(false);
               
                OnUnitSelected?.Invoke(this, new UnitSelectData(UnitIndex,SlotIndex, null,0));
                

            }
            Debug.Log(_unit.Name);
        });
       
        
    }
    public void UnSelected()
    {
        isSelected=false;
        Selected.SetActive(false);
    }

}

public class UnitSelectData
{
    public int Index;
    public AllianceUnit Unit;
    public int SkillIndex;
    public int SlotIndex;
    public UnitSelectData(int i,int slot, AllianceUnit u, int si) => (Index,SlotIndex, Unit, SkillIndex) = (i, slot, u, si);
}