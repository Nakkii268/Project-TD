using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineUpUI : UICanvas
{
    [SerializeField] private List<LineUpSlot> _slots;
    [SerializeField] private List<AllianceUnit> _playerSquad;
    [SerializeField] private Button _backBtn;
    [SerializeField] private Button _homeBtn;
    private void Start()
    {
        //get player squad 
        for (int i = 0; i < _playerSquad.Count; i++)
        {
            _slots[i].Initialized(_playerSquad[i]);
        }
        _backBtn.onClick.AddListener(() =>
        {
            Close(0);
        });
    }
}
