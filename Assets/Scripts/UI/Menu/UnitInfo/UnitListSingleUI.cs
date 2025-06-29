using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitListSingleUI : MonoBehaviour
{
    [SerializeField] private AllianceUnit _unit;
    [SerializeField] private Button _btn;

    [SerializeField] public UnitListUI _unitListUI;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image ClassIcon;
    [SerializeField] private Image RarityIcon;
    [SerializeField] private Image UIPotrait;
    [SerializeField] private Image LBIcon;
    

    public void Initialized(AllianceUnit unit)
    {
        
        _unit = unit;
        _nameText.text = unit.Name;
        _levelText.text = unit.Level.ToString();
        ClassIcon.sprite = unit.UnitClass.ClassIcon;
        RarityIcon.sprite = unit.Rarity.RarityIcon;
        UIPotrait.sprite = unit.unitUIPotrait;
        LBIcon.sprite = GameManager.Instance.limitBreakIcon.GetIcon(unit.LimitBreak);

    }

    private void Start()
    {
        
        _btn.onClick.AddListener(() =>
        {
            UIManager.Instance.OpenUI<CharacterInfoUI>(_unit);
        });
     
      
    }
}
