using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitListSingleUI : MonoBehaviour
{
    [SerializeField] private AllianceUnit _unit;
    [SerializeField] private Button _btn;
    [SerializeField] public UnitListUI _unitListUI;

    private void Start()
    {
        _btn.onClick.AddListener(() =>
        {
            _unitListUI.characterInfoUI.Initialized(_unit);
            _unitListUI.characterInfoUI.gameObject.SetActive(true);
        });
    }
}
