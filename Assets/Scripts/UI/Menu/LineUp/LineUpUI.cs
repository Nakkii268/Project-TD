using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LineUpUI : UICanvas
{
    [SerializeField] private List<LineUpSlot> _slots;
    [SerializeField] private List<LineUpSave> _playerSquad= new List<LineUpSave>();
    [SerializeField] public Dictionary<int,LineUpSave> _tempSquad = new Dictionary<int, LineUpSave>();

    [SerializeField] private Button _backBtn;
    [SerializeField] private Button _homeBtn;
    
    private void Start()
    {
        //get player squad 
         
        _backBtn.onClick.AddListener(() =>
        {
            _tempSquad.Clear();

            UIManager.Instance.Close<LineUpUI>(0);
        });
        _homeBtn.onClick.AddListener(() => {
            _tempSquad.Clear();
            UIManager.Instance.CloseToHome();
            UIManager.Instance.OpenUI<MenuUI>();
        });
      
        _tempSquad = _playerSquad.ToDictionary(item => item.Index);
        
    }

  
    public override void SetUp()
    {
        Initialized();
    }
    private void Initialized()
    {
        _playerSquad = GameManager.Instance._playerDataManager.PlayerDataSO.PlayerLineUp;
        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].OnUnitAssign += LineUpUI_OnUnitAssign;
            _slots[i].Initialized(null,-1);
            _slots[i].IndexAssign(i);
        }
        for (int i = 0; i < _playerSquad.Count; i++)
        {
            _slots[_playerSquad[i].Index].Initialized(_playerSquad[i].Unit, _playerSquad[i].SkillIndex);
            
            
        }
        
    }

    private void LineUpUI_OnUnitAssign(object sender, LineUpSave e)
    {
       Debug.Log(e.SkillIndex +"lineup");
        if (_tempSquad.ContainsKey(e.Index))
        {
            if (e.Unit == null) //if unit =null => unit remove
            {
                _tempSquad.Remove(e.Index);
                GameManager.Instance._playerDataManager.PlayerDataSO.UpdateLineUp(e.Unit, e.Index, e.SkillIndex);
             
                Debug.Log("case1");
                return;
            }
            //case unit update
            GameManager.Instance._playerDataManager.PlayerDataSO.UpdateLineUp(e.Unit, e.Index, e.SkillIndex);
        
            Debug.Log("case2");
                Debug.Log(e.SkillIndex +"before");

            _tempSquad[e.Index] = e;
                Debug.Log(_tempSquad[e.Index].SkillIndex+ "after");

            return;
        }
        //case add new unit
        GameManager.Instance._playerDataManager.PlayerDataSO.UpdateLineUp(e.Unit, e.Index, e.SkillIndex);
      
                Debug.Log("case3");

        _tempSquad.Add(e.Index, e);
        
        
        
    }
}
[Serializable] 
public class LineUpSave
{
    public int Index;
    public AllianceUnit Unit;
    public int SkillIndex;
    public LineUpSave(int i, AllianceUnit u,int si) => (Index, Unit,SkillIndex) = (i, u,si);
}