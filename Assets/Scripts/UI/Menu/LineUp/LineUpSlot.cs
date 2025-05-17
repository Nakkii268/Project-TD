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
    [SerializeField] public UnitSelectionUI _unitSelectionUI; // change to unitSelectionUI
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image ClassIcon;
    [SerializeField] private Image RarityIcon;
    [SerializeField] private Image UIPotrait;
    [SerializeField] private Image NullPotrait;
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
    }
    private void Start()
    {
        _btn.onClick.AddListener(() =>
        {

            _unitSelectionUI.gameObject.SetActive(true);
        });
    }
}

