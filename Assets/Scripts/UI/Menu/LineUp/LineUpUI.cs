using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LineUpUI : UICanvas
{
    [SerializeField] private List<LineUpSlot> _slots;
    [SerializeField] private List<LineUpSave> _playerSquad;
    [SerializeField] public Dictionary<string,LineUpSave> _tempSquad = new Dictionary<string, LineUpSave>();

    [SerializeField] private Button _backBtn;
    [SerializeField] private Button _homeBtn;
    [SerializeField] private Button SaveBtn;
    private void Start()
    {
        //get player squad 
        
        _backBtn.onClick.AddListener(() =>
        {
            UIManager.Instance.Close<LineUpUI>(0);
        });
        _homeBtn.onClick.AddListener(() => {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<MenuUI>();
        });
        SaveBtn.onClick.AddListener(() => { 
            _playerSquad = _tempSquad.Values.ToList();
            
        });
        _tempSquad = _playerSquad.ToDictionary(item => item.Unit.UnitID);
    }

    public override void SetUp()
    {
        Initialized();
    }
    private void Initialized()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].OnUnitAssign += LineUpUI_OnUnitAssign;
            _slots[i].Initialized(null);
            _slots[i].IndexAssign(i);
        }
        for (int i = 0; i < _playerSquad.Count; i++)
        {
            _slots[_playerSquad[i].Index].Initialized(_playerSquad[i].Unit);
            
            
        }
        
    }

    private void LineUpUI_OnUnitAssign(object sender, LineUpSave e)
    {
        
        if (_tempSquad.ContainsKey(e.Unit.UnitID))
        {
            if (e.Index == -1)
            {
                _tempSquad.Remove(e.Unit.UnitID);

                return;
            }
            _tempSquad[e.Unit.UnitID] = e;
            return;
        }
        _tempSquad.Add(e.Unit.UnitID, e);
        
    }
}
[Serializable] 
public class LineUpSave
{
    public int Index;
    public AllianceUnit Unit;
    public LineUpSave(int i, AllianceUnit u) => (Index, Unit) = (i, u);
}